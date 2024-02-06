using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Volue_case.Models;
using Volue_case.Models.ViewModels;
using Volue_case.Repositories;
using Volue_case.Services.CustomerService;

namespace Volue_case.Services.BidResultService;

public class BidResultService(IUnitOfWork unitOfWork, ICustomerService customer, IHttpClientFactory http,
    IConfiguration config, IMapper mapper) : IBidResultService
{
    private const string BaseUrl = "https://vmsn-app-planner3test.azurewebsites.net/status/market/bid-result";

    public async Task<Bid?> FetchDataAsync(BidDefaultQueryDto dto)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{BaseUrl}?" +
            $"ForDate={dto.Day}&Market={dto.Market}&CustomerId={dto.CustomerId}&Country={dto.Country}");

        request.Headers.Add("ApiKey", config.GetSection("API_KEY").Value);

        var client = http.CreateClient();

        var response = await client.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        try
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            var parsedBid = JsonSerializer.Deserialize<Bid>(responseContent, jsonOptions);

            return parsedBid;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error during parsing result! {e.Message}");
        }
    }

    public async Task<Bid?> GetByIdAsync(string id) => await unitOfWork.Bid.GetById(id).SingleOrDefaultAsync();

    public async Task<string?> GetCustomerIdByBid(string bidId) =>
        await unitOfWork.Bid.GetById(bidId)
            .SelectMany(x=>x.Series.Select(s=> s.CustomerId)).Distinct()
            .FirstOrDefaultAsync();

    public async Task<BidDetailedVm?> GetDetailedVmByIdAsync(string id) =>
        await unitOfWork.Bid.GetById(id).ProjectTo<BidDetailedVm>(mapper.ConfigurationProvider).SingleOrDefaultAsync();

    public async Task<List<Bid>> GetAllAsync() => await unitOfWork.Bid.GetAll().ToListAsync();
    public async Task<List<BidBasicVm>> GetAllBasicVmAsync() => 
        await unitOfWork.Bid.GetAll().ProjectTo<BidBasicVm>(mapper.ConfigurationProvider).ToListAsync();

    public async Task<List<BidDetailedVm>> GetAllDetailedVmAsync() =>
        await unitOfWork.Bid.GetAll().ProjectTo<BidDetailedVm>(mapper.ConfigurationProvider).ToListAsync();

    public async Task<List<HistoryVm>> GetHistoryAsync(string bidId) => await unitOfWork.Bid.GetHistoryByBidId(bidId)
        .ProjectTo<HistoryVm>(mapper.ConfigurationProvider)
        .ToListAsync();

    public async Task AddNewIfNotExist(Bid bid)
    {
        var random = new Random();
        
        if (await IsBidExistAsync(bid.ExternalId))
            return;

        var customerId = bid.Series.Select(x => x.CustomerId).Distinct().FirstOrDefault();
        if (string.IsNullOrEmpty(customerId))
            throw new NullReferenceException("Customer id is null");

        await customer.AddNewIfNotExistAsync(customerId);



        foreach (var history in bid.UpdateHistory)
            history.BidExternalId = bid.ExternalId;

        foreach (var series in bid.Series)
        {
            series.BidExternalId = bid.ExternalId;
            series.Status = series.Status == BidStatus.Undefined
                ? BidStatus.PendingConfirmation
                : series.Status;

            series.Price = series.Price <= 0 ? random.Next(10, 35) : series.Price;
            series.Currency = "USD";
            
            foreach (var pos in series.Positions){
                pos.SeriesExternalId = series.ExternalId;
                pos.Quantity = pos.Quantity <= 0 ? random.Next(1, 5) : pos.Quantity;
            }
        }

        await unitOfWork.Bid.AddAsync(bid);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteById(string id)
    {
        var bid = await unitOfWork.Bid.GetById(id).SingleOrDefaultAsync();
        if(bid == null)
            return;

        unitOfWork.Bid.Delete(bid);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAllAsync() { 
        unitOfWork.Bid.DeleteAll(await unitOfWork.Bid.GetAll().ToListAsync());
        unitOfWork.Customer.DeleteAll(await unitOfWork.Customer.GetAll().ToListAsync());
        await unitOfWork.SaveAsync();
    }


    public async Task<bool> IsBidExistAsync(string id) => await unitOfWork.Bid.GetById(id).AnyAsync();
    public async Task<bool> AnyAsync() => await unitOfWork.Bid.GetAll().AnyAsync();

    public bool CheckTwoBidsIdentical(BidDetailedVm bid1, Bid bid2)
    {
        var propIdentical = bid1.Id == bid2.ExternalId && bid1.Country == bid2.Country
            && bid1.Market == bid2.Market
            && bid1.Status == bid2.Status;
        var seriesNumber = bid1.Series.Count == bid2.Series.Count;
        var positionsCount =
            bid1.Series.Select(x => x.Positions).Count() == bid2.Series.Select(x => x.Positions).Count();
        var history = bid1.UpdateHistory.Count == bid2.UpdateHistory.Count;

        return propIdentical && seriesNumber && positionsCount && history;
    }
}