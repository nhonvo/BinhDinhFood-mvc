using System.Diagnostics;
using System.Globalization;
using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Presentation.Models.Result;
using Microsoft.AspNetCore.Mvc;

namespace BinhDinhFood.Controllers;

public class HomeController : Controller
{
    readonly CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    private readonly IProductRepository _repo;
    public HomeController(IProductRepository repo)
    {
        _repo = repo;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Error(int statuscode)
    {
        return statuscode == 404
            ? View("NotFound")
            : (IActionResult)View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult About()
    {
        return View();

    }
    public IActionResult Help()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }

}
