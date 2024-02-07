using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Volue_case.Models;
using Volue_case.Models.Entities;
using Volue_case.Models.ViewModels;
using Volue_case.Repositories;
using Volue_case.Services.CustomerService;

namespace Volue_case.Services.BidResultService;

public class BidResultService(IUnitOfWork unitOfWork, ICustomerService customer, IHttpClientFactory http,
    IConfiguration config, IMapper mapper) : IBidResultService
{
    // Volue endpoint base url
    private const string BaseUrl = "https://vmsn-app-planner3test.azurewebsites.net/status/market/bid-result";

    public async Task<Bid?> FetchDataAsync(BidDefaultQueryDto dto)
    {
        // Init http get request
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{BaseUrl}?" +
            $"ForDate={dto.Day}&Market={dto.Market}&CustomerId={dto.CustomerId}&Country={dto.Country}");

        // Add authentication value(s)
        request.Headers.Add("ApiKey", config.GetSection("API_KEY").Value);

        var client = http.CreateClient();

        // Send async request
        var response = await client.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.OK) // The code must be 200 in order to pass.
            return null;

        try
        {
            // Parse response
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Handle the case when server response with 200, but the result was empty anyway.
            responseContent = responseContent.Replace("\"dateOfLastChange\":null",
                "\"dateOfLastChange\":\"0001-01-01T00:00:00\"");
            if (responseContent.Contains("\"externalId\":\"\""))
                return null;


            
            

            // Create json parsing options
            // ToDo :: An error shown up in some cases when parsing some DateTimes
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            // Deserialize json into Bid model.
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

    // ToDo :: This line causes database too many records issue (Projection configuration / profiles must be revisited)
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
        var random = new Random(); // Random is needed for simulate data manipulation later on.
        
        if (await IsBidExistAsync(bid.ExternalId))
            return;

        var customerId = bid.Series.Select(x => x.CustomerId).Distinct().FirstOrDefault();
        if (string.IsNullOrEmpty(customerId))
            throw new NullReferenceException("Customer id is null");

        // Create customer (assuming a bid with all its series has the exact same customer).
        await customer.AddNewIfNotExistAsync(customerId);
        
        // Add new history record
        bid.UpdateHistory.Add(
            new UpdateHistory
            {
                FromStatus = bid.UpdateHistory.LastOrDefault()?.ToStatus?? BidStatus.Undefined, 
                ToStatus = BidStatus.PulledByAamer, UpdateTime = DateTime.Now
            });

        foreach (var history in bid.UpdateHistory)
            history.BidExternalId = bid.ExternalId; // Set history BidId foreign key.

        foreach (var series in bid.Series)
        {
            series.BidExternalId = bid.ExternalId; // Set series BidId foreign key.
            
            
            // Simulate data manipulation (as requested in the case description).
            series.Status = series.Status == BidStatus.Undefined
                ? BidStatus.PendingConfirmation
                : series.Status;
            series.Price = series.Price <= 0 ? random.Next(10, 35) : series.Price;
            series.Currency = "USD";
            
            foreach (var pos in series.Positions){
                pos.SeriesExternalId = series.ExternalId; // Set position SeriesId foreign key
                pos.Quantity = pos.Quantity <= 0 ? random.Next(1, 5) : pos.Quantity; // A part of data manipulation
            }
        }

        // Save entity to database.
        await unitOfWork.Bid.AddAsync(bid);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteById(string id)
    {
        var bid = await unitOfWork.Bid.GetById(id).SingleOrDefaultAsync();
        if(bid == null) // Handle the case where the bid is already deleted, or not found for some reason (Request sent twice for example).
            return;

        // Perform delete.
        unitOfWork.Bid.Delete(bid);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAllAsync() { 
        // This method assumes that the database has only Bid and Customer.
        // However, Series, Positions, and UpdateHistory will be auto deleted with Bid, due to Cascade configuration in DbContext.
        unitOfWork.Bid.DeleteAll(await unitOfWork.Bid.GetAll().ToListAsync());
        unitOfWork.Customer.DeleteAll(await unitOfWork.Customer.GetAll().ToListAsync());
        await unitOfWork.SaveAsync();
    }


    public async Task<bool> IsBidExistAsync(string id) => await unitOfWork.Bid.GetById(id).AnyAsync();
    public async Task<bool> AnyAsync() => await unitOfWork.Bid.GetAll().AnyAsync();

    public bool CheckTwoBidsIdentical(BidDetailedVm bid1, Bid bid2)
    {
        // This method simulate the check of two Bid results are identical.
        // The check is performed on some properties, and thus it is not an accurate result as expected.
        // Consider two objects must have the same type (BidDetailedVm chosen to make it easy to call from controller).
        // The DateTime comparison is always false, due to Volue server time-zone (must consider time-zone in order to check dates equality).
        
        var propIdentical = bid1.Id == bid2.ExternalId && bid1.Country == bid2.Country
            && bid1.Market == bid2.Market
            && bid1.Status == bid2.Status;
        var seriesNumber = bid1.Series.Count == bid2.Series.Count;
        var positionsCount =
            bid1.Series.Select(x => x.Positions).Count() == bid2.Series.Select(x => x.Positions).Count();
        var history = bid1.UpdateHistory.Count == bid2.UpdateHistory.Count + 1; // +1 added because we add a new history record on adding (manipulate data)

        return propIdentical && seriesNumber && positionsCount && history;
    }
}