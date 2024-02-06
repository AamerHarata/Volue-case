using Microsoft.AspNetCore.Mvc;

namespace Volue_case.Controllers;

public class BidResultController : Controller
{
    public async Task<IActionResult> ListAll()
    {
        return Ok();
    }

    public async Task<IActionResult> Get(string id)
    {
        return Ok();
    }
}