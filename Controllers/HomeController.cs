using Microsoft.AspNetCore.Mvc;
using Volue_case.Models;

namespace Volue_case.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var bidResult = new Bid();
        
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://vmsn-app-planner3test.azurewebsites.net/status/market/bid-result?" +
            "ForDate=2024-02-03&Market=FCR-D-D1&CustomerId=TestCustomer&Country=Sweden");
        request.Headers.Add("ApiKey", "Api-Planner-996ba74c-cdaf-4f66-9448-ffff");

        var client = _clientFactory.CreateClient();

        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        return Ok(responseContent);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
