using Microsoft.AspNetCore.Mvc;
using Volue_case.Services.BidResultService;
using Volue_case.Services.CustomerService;

namespace Volue_case.Controllers;

public class DataManagerController(IBidResultService bid, ICustomerService customer) : Controller
{

    public async Task<IActionResult> Index()
    {
        if (!await bid.AnyAsync())
            ViewBag.Message = "No data found!";
        return View();
    }
    public async Task<IActionResult> DeleteAllData()
    {
        await bid.DeleteAllAsync();
        await customer.DeleteAllAsync();
        return RedirectToAction("Index");
    }
}