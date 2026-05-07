using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Models;
using System.Diagnostics;

namespace RestaurantOrderingSystem.Controllers;

public class HomeController(RestaurantDbContext context, ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var featuredItems = await context.MenuItems
            .Include(item => item.Category)
            .Where(item => item.IsAvailable && item.IsFeatured)
            .OrderBy(item => item.Name)
            .ToListAsync();

        return View(featuredItems);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
