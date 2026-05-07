using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Extensions;
using RestaurantOrderingSystem.Models;
using RestaurantOrderingSystem.ViewModels;

namespace RestaurantOrderingSystem.Controllers;

public class OrdersController(RestaurantDbContext context) : Controller
{
    private const string CartSessionKey = "RestaurantCart";

    public async Task<IActionResult> Index()
    {
        var orders = await context.Orders
            .Include(order => order.Items)
            .OrderByDescending(order => order.OrderedAt)
            .ToListAsync();

        return View(orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await context.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);

        return order is null ? NotFound() : View(order);
    }

    public IActionResult Checkout()
    {
        var cart = GetCart();
        if (cart.Items.Count == 0)
        {
            TempData["ErrorMessage"] = "Your cart is empty. Add a dish before checking out.";
            return RedirectToAction("Index", "Menu");
        }

        return View(new CheckoutViewModel { Cart = cart });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
    {
        var cart = GetCart();
        viewModel.Cart = cart;

        if (cart.Items.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Your cart is empty.");
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var order = new Order
        {
            CustomerName = viewModel.CustomerName,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            DeliveryAddress = viewModel.DeliveryAddress,
            Notes = viewModel.Notes,
            Subtotal = cart.Subtotal,
            DeliveryFee = cart.DeliveryFee,
            Tax = cart.Tax,
            Total = cart.Total,
            Items = cart.Items.Select(item => new OrderItem
            {
                MenuItemId = item.MenuItemId,
                ItemName = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                LineTotal = item.LineTotal
            }).ToList()
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        HttpContext.Session.Remove(CartSessionKey);

        return RedirectToAction(nameof(Confirmation), new { id = order.Id });
    }

    public async Task<IActionResult> Confirmation(int id)
    {
        var order = await context.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);

        return order is null ? NotFound() : View(order);
    }

    private CartViewModel GetCart()
    {
        return HttpContext.Session.GetObject<CartViewModel>(CartSessionKey) ?? new CartViewModel();
    }
}
