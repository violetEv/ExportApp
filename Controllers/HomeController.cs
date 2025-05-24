using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExportApp.Models;

namespace ExportApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    private List<DataModel> AmbilData()
    {
        return new List<DataModel>
        {
            new DataModel { Nama = "Marry", Tanggal = DateTime.Now },
            new DataModel { Nama = "Jane", Tanggal = DateTime.Now }
        };
    }

    public IActionResult Index()
    {
        var data = AmbilData();
        return View(data);
    }
    public IActionResult Privacy()
    {
        return View();
    }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
