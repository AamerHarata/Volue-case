using Microsoft.AspNetCore.Mvc;
using Volue_case.Models.ViewModels;
using Volue_case.Services.BidResultService;
using Volue_case.Services.CommonHelpers;

namespace Volue_case.Controllers;

[Route("[controller]")]
public class HomeController(IBidResultService bid) : Controller
{

    [HttpGet("/")]
    public async Task<IActionResult> Index() => View(await bid.GetAllBasicVmAsync());

    
    [HttpPost("/")]
    public async Task<IActionResult> Index(string customerId, DateTime day, string market, string country)
    {
        ViewBag.CustomerId = customerId;
        ViewBag.Market = market;
        ViewBag.Country = country;
        ViewBag.Day = day.ToDateRequestFormat();
        
        // Get existing results in database
        var existingResults = await bid.GetAllBasicVmAsync();
        
        // Fetch data from Volue api that match the query parameters
        var dto = new BidDefaultQueryDto(customerId, day, market, country);
        var result = await bid.FetchDataAsync(dto);
        
        if (result == null) // Result not found in Volue api
        { 
            ViewBag.Message = "Result not found in Volue api!";
            return View(existingResults);
        }

        if (await bid.IsBidExistAsync(result.ExternalId)) // Result found but already saved into database
        { 
            ViewBag.Message = "Result found! Already exists in our database!";
            return View(existingResults);
        }

        // Result found in Volue api, but not in database. Save the result!
        await bid.AddNewIfNotExist(result);
        existingResults = await bid.GetAllBasicVmAsync();
        ViewBag.Message = "New result found and added to the database!";

        return View(existingResults);
    }

    [HttpGet("BidResult/{id}")]
    // ToDo :: This line issues database issue (Too much records fetched / possible projection configuration).
    public async Task<IActionResult> BidResult(string id) => View(await bid.GetDetailedVmByIdAsync(id));

    [HttpGet("CheckStatus/{resultId}")]
    public async Task<IActionResult> CheckStatus(string resultId)
    {
        // Get result by id from database
        var existingResult = await bid.GetDetailedVmByIdAsync(resultId);
        var customerId = await bid.GetCustomerIdByBid(resultId);
        
        if (existingResult == null)
            return NotFound($"Not found result with id {resultId}");
        if (string.IsNullOrEmpty(customerId))
            return NotFound("Customer id error!");

        // Try fetch similar result from Volue api.
        var result = await bid.FetchDataAsync(new BidDefaultQueryDto(customerId, existingResult.DayAsDate,
            existingResult.Market, existingResult.Country));

        if (result == null) // Result was saved in db but no longer in Volue api (it means deleted from Volue).
            return Ok("Result was deleted from Volue api!");

        // Check if both db and Volue results are similar (This is checking equality as simulation, but not accurately).
        var identicalWithExistingResult = bid.CheckTwoBidsIdentical(existingResult, result);


        // Return Ok status with appropriate message.
        return identicalWithExistingResult
            ? Ok("Both results in db and Volue api are identical! No sync needed.")
            : Ok("Result seem to be changed! Click Sync to override result in db.");
    }

    [HttpGet("History/{bidId}")]
    public async Task<IActionResult> History(string bidId) => View(await bid.GetHistoryAsync(bidId));

    [HttpPatch("SyncBidResult/{id}")]
    // ToDo :: This endpoint caused errors (when changing the market value in our db). The error may be related to parsing DateTime to json somewhere!
    public async Task<IActionResult> SyncBidResult(string id)
    {
        // ToDo :: This line issues database issue (Too much records fetched / possible projection configuration).
        // Get existing result from db.
        var existingResult = await bid.GetDetailedVmByIdAsync(id);
        var customerId = await bid.GetCustomerIdByBid(id);
        if (existingResult == null)
            return NotFound($"Not found result with id {id}");
        if (string.IsNullOrEmpty(customerId))
            return NotFound("Customer id error!");

        // Fetch similar result from Volue (this should be fetched by Id in order to be more accurate query).
        var result = await bid.FetchDataAsync(new BidDefaultQueryDto(customerId, existingResult.DayAsDate,
            existingResult.Market, existingResult.Country));

        if (result == null) // Result not found in Volue api
            return Ok("Result was deleted from Volue api!");
        
        var identicalWithExistingResult = bid.CheckTwoBidsIdentical(existingResult, result);

        if (identicalWithExistingResult) // Result found, but it's identical with the data in db. No action needed
            return Ok("Both results in db and Volue api are identical! No sync needed.");

        // Simulate Sync by deleting and recreating result (Not the optimal way to update or sync existing data).
        await bid.DeleteById(id);
        await bid.AddNewIfNotExist(result);

        return Ok("Sync completed!");
    }
    

}
