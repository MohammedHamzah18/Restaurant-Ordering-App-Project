using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.ViewModels;

namespace RestaurantOrderingSystem.Controllers;

public class MenuController(RestaurantDbContext context) : Controller
{
    public async Task<IActionResult> Index(int? categoryId, string? search)
    {
        var query = context.MenuItems
            .Include(item => item.Category)
            .Where(item => item.IsAvailable);

        if (categoryId.HasValue)
        {
            query = query.Where(item => item.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(item => item.Name.Contains(search) || item.Description.Contains(search));
        }

        var viewModel = new MenuIndexViewModel
        {
            Categories = await context.Categories.OrderBy(category => category.Name).ToListAsync(),
            MenuItems = await query.OrderByDescending(item => item.IsFeatured).ThenBy(item => item.Name).ToListAsync(),
            SelectedCategoryId = categoryId,
            SearchTerm = search
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await context.MenuItems
            .Include(menuItem => menuItem.Category)
            .FirstOrDefaultAsync(menuItem => menuItem.Id == id && menuItem.IsAvailable);

        return item is null ? NotFound() : View(item);
    }
}
