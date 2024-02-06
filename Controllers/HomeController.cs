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

[Route("[controller]")]
public class HomeController(IBidResultService bid) : Controller
{

    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        var existingResults = await bid.GetAllBasicVmAsync();

        return View(existingResults);
    }

    
    [HttpPost("/")]
    public async Task<IActionResult> Index(string customerId, DateTime day, string market, string country)
    {
        var existingResults = await bid.GetAllBasicVmAsync();

        var dto = new BidDefaultQueryDto(customerId, day, market, country);
        var result = await bid.FetchDataAsync(dto);

        if (result == null)
        {
            ViewBag.Message = "Result not found in Volue api!";
            return View(existingResults);
        }

        if (await bid.IsBidExistAsync(result.ExternalId))
        {
            ViewBag.Message = "Result found! Already exists in our database!";
            return View(existingResults);
        }

        await bid.AddNewIfNotExist(result);
        existingResults = await bid.GetAllBasicVmAsync();
        ViewBag.Message = "New result found and added to the database!";

        return View(existingResults);
    }

    [HttpGet("BidResult/{id}")]
    public async Task<IActionResult> BidResult(string id)
    {
        var result = await bid.GetDetailedVmByIdAsync(id);

        return View(result);
    }

    [HttpGet("CheckStatus/{resultId}")]
    public async Task<IActionResult> CheckStatus(string resultId)
    {
        var existingResult = await bid.GetDetailedVmByIdAsync(resultId);
        var customerId = await bid.GetCustomerIdByBid(resultId);
        if (existingResult == null)
            return NotFound($"Not found result with id {resultId}");
        if (string.IsNullOrEmpty(customerId))
            return NotFound("Customer id error!");

        var result = await bid.FetchDataAsync(new BidDefaultQueryDto(customerId, existingResult.DayAsDate,
            existingResult.Market, existingResult.Country));

        if (result == null)
            return Ok("Result was deleted from Volue api!");


        var identicalWithExistingResult = bid.CheckTwoBidsIdentical(existingResult, result);


        return identicalWithExistingResult
            ? Ok("Both results in db and Volue api are identical! No sync needed.")
            : Ok("Result seem to be changed! Click Sync to override result in db.");
    }

    [HttpGet("History/{bidId}")]
    public async Task<IActionResult> History(string bidId) => View(await bid.GetHistoryAsync(bidId));

    [HttpPatch("SyncBidResult/{id}")]
    public async Task<IActionResult> SyncBidResult(string id)
    {
        var existingResult = await bid.GetDetailedVmByIdAsync(id);
        var customerId = await bid.GetCustomerIdByBid(id);
        if (existingResult == null)
            return NotFound($"Not found result with id {id}");
        if (string.IsNullOrEmpty(customerId))
            return NotFound("Customer id error!");

        var result = await bid.FetchDataAsync(new BidDefaultQueryDto(customerId, existingResult.DayAsDate,
            existingResult.Market, existingResult.Country));

        if (result == null)
            return Ok("Result was deleted from Volue api!");
        
        var identicalWithExistingResult = bid.CheckTwoBidsIdentical(existingResult, result);

        if (identicalWithExistingResult)
            return Ok("Both results in db and Volue api are identical! No sync needed.");

        await bid.DeleteById(id);
        await bid.AddNewIfNotExist(result);

        return Ok("Sync completed!");
    }
    

}
