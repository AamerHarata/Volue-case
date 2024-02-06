using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Volue_case.Models;
using Volue_case.Models.ViewModels;
using Volue_case.Repositories;
using Volue_case.Services.CustomerService;

namespace Volue_case.Services.BidResultService;

public class BidResultService(IUnitOfWork unitOfWork, ICustomerService customer, IHttpClientFactory http,
    IConfiguration config) : IBidResultService
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

    public async Task<List<Bid>> GetAllAsync() => await unitOfWork.Bid.GetAll().ToListAsync();

    public async Task AddNewIfNotExist(Bid bid)
    {
        if (await IsBidExistAsync(bid.ExternalId))
            return;

        var customerId = bid.Series.Select(x => x.CustomerId).Distinct().FirstOrDefault();
        if (string.IsNullOrEmpty(customerId))
            throw new NullReferenceException("Customer id is null");

        await customer.AddNewIfNotExist(customerId);



        foreach (var history in bid.UpdateHistory)
            history.BidExternalId = bid.ExternalId;

        foreach (var series in bid.Series)
        {
            series.BidExternalId = bid.ExternalId;
            foreach (var pos in series.Positions)
                pos.SeriesExternalId = series.ExternalId;
        }

        await unitOfWork.Bid.AddAsync(bid);
        await unitOfWork.SaveAsync();
    }


    private async Task<bool> IsBidExistAsync(string id) => await unitOfWork.Bid.GetById(id).AnyAsync();

}