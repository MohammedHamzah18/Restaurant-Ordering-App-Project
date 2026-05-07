using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Extensions;
using RestaurantOrderingSystem.ViewModels;

namespace RestaurantOrderingSystem.Controllers;

public class CartController(RestaurantDbContext context) : Controller
{
    private const string CartSessionKey = "RestaurantCart";

    public IActionResult Index()
    {
        return View(GetCart());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int menuItemId, int quantity = 1)
    {
        var menuItem = await context.MenuItems.FirstOrDefaultAsync(item => item.Id == menuItemId && item.IsAvailable);
        if (menuItem is null)
        {
            return NotFound();
        }

        var cart = GetCart();
        var existingItem = cart.Items.FirstOrDefault(item => item.MenuItemId == menuItemId);
        if (existingItem is null)
        {
            cart.Items.Add(new CartItemViewModel
            {
                MenuItemId = menuItem.Id,
                Name = menuItem.Name,
                UnitPrice = menuItem.Price,
                Quantity = Math.Clamp(quantity, 1, 99),
                ImageUrl = menuItem.ImageUrl
            });
        }
        else
        {
            existingItem.Quantity = Math.Clamp(existingItem.Quantity + quantity, 1, 99);
        }

        SaveCart(cart);
        TempData["SuccessMessage"] = $"{menuItem.Name} added to your cart.";

        return RedirectToAction("Index", "Menu");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int menuItemId, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(cartItem => cartItem.MenuItemId == menuItemId);
        if (item is not null)
        {
            item.Quantity = Math.Clamp(quantity, 1, 99);
            SaveCart(cart);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int menuItemId)
    {
        var cart = GetCart();
        cart.Items.RemoveAll(item => item.MenuItemId == menuItemId);
        SaveCart(cart);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        HttpContext.Session.Remove(CartSessionKey);
        return RedirectToAction(nameof(Index));
    }

    internal CartViewModel GetCart()
    {
        return HttpContext.Session.GetObject<CartViewModel>(CartSessionKey) ?? new CartViewModel();
    }

    internal void SaveCart(CartViewModel cart)
    {
        HttpContext.Session.SetObject(CartSessionKey, cart);
    }
}
