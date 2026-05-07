using RestaurantOrderingSystem.Models;

namespace RestaurantOrderingSystem.ViewModels;

public class MenuIndexViewModel
{
    public IReadOnlyList<Category> Categories { get; set; } = [];

    public IReadOnlyList<MenuItem> MenuItems { get; set; } = [];

    public int? SelectedCategoryId { get; set; }

    public string? SearchTerm { get; set; }
}
