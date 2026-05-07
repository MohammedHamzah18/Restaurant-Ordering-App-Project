namespace RestaurantOrderingSystem.ViewModels;

public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = [];

    public decimal Subtotal => Items.Sum(item => item.LineTotal);

    public decimal DeliveryFee => Items.Count == 0 ? 0 : 49;

    public decimal Tax => Math.Round(Subtotal * 0.05m, 2);

    public decimal Total => Subtotal + DeliveryFee + Tax;

    public int TotalItems => Items.Sum(item => item.Quantity);
}
