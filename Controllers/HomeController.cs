using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Volue_case.Models;

namespace Volue_case.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IMapper mapper)
    {
        _logger = logger;
        _clientFactory = clientFactory;
        _mapper = mapper;
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
        // var responseContent = await response.Content.ReadFromJsonAsync<Bid>();

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        // var x = _mapper.Map<Bid>(responseContent);

        var x = JsonSerializer.Deserialize<Bid>(responseContent, options);

        return View(x);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
