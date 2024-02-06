using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volue_case.Data;
using Volue_case.Models;
using Volue_case.Models.Entities;
using Volue_case.Models.ViewModels;
using Volue_case.Services.BidResultService;

namespace Volue_case.Controllers;

public class HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IMapper mapper,
        IConfiguration configuration, ApplicationDbContext context, IBidResultService bid) : Controller
{

    public async Task<IActionResult> Index()
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://vmsn-app-planner3test.azurewebsites.net/status/market/bid-result?" +
            "ForDate=2024-02-03&Market=FCR-D-D1&CustomerId=TestCustomer&Country=Sweden");
        request.Headers.Add("ApiKey", "Api-Planner-996ba74c-cdaf-4f66-9448-ffff");

        var client = clientFactory.CreateClient();

        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        // var responseContent = await response.Content.ReadFromJsonAsync<Bid>();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };
        

        var parsedBid = JsonSerializer.Deserialize<Bid>(responseContent, options);

        if (parsedBid == null)
            return BadRequest("Parsed Bid is null");
        
        // Save to db
        if (!await context.Bids.AnyAsync(x => x.ExternalId == parsedBid.ExternalId))
        {
            var customerId = parsedBid.Series.Select(x => x.CustomerId).Distinct().SingleOrDefault();
            if (customerId != null && !await context.Customers.AnyAsync(x => x.Id == customerId))
                await context.AddAsync(new Customer(customerId));


            foreach (var history in parsedBid.UpdateHistory)
                history.BidExternalId = parsedBid.ExternalId;
            foreach (var series in parsedBid.Series){
                series.BidExternalId = parsedBid.ExternalId;
                foreach (var pos in series.Positions)
                    pos.SeriesExternalId = series.ExternalId;
            }

            await context.AddAsync(parsedBid);
            await context.SaveChangesAsync();
        }
        

        return View(parsedBid);
    }

    public async Task<IActionResult> Privacy()
    {
        var bidResult = await bid.FetchDataAsync(
            new BidDefaultQueryDto("TestCustomer", new DateTime(2024, 2, 3), 
                "FCR-D-D1", "Sweden"));
        return Ok(bidResult);
    }

    public async Task<IActionResult> Test()
    {

        var res = await context.Bids.ProjectTo<BidVm>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        
        return Ok(res);
    }


}
