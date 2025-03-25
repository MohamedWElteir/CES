using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CES.Models;

namespace CES.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}