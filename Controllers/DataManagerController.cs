using Microsoft.AspNetCore.Mvc;
using Volue_case.Models.ViewModels;

namespace Volue_case.Controllers;

public class DataManagerController : Controller
{
    public async Task<IActionResult> DeleteAllData()
    {
        return BadRequest();
    }

    [HttpGet]
    public IActionResult SyncListener()
    {
        // Get stored data in database

        // Get data from api call.

        // Check if still exist in api? Check if is stored in database?

        // Check changes in: number in Series and their Positions? Last update?

        // In case any changes, override the existing data with the new data.

        return Ok();
    }

    [HttpPost]
    public IActionResult SyncListener(BidDefaultQueryDto dto)
    {


        return Ok();
    }

}