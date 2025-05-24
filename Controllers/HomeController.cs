using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExportApp.Models;
using ExportApp.Data;

namespace ExportApp.Controllers;

public class HomeController : Controller
{
        private readonly IDataRepository _repository;

        public HomeController(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetDataAsync();
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
