using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderingSystem.ViewModels;

public class CheckoutViewModel
{
    [Required, StringLength(120), Display(Name = "Full name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(180)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(30), Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, StringLength(250), Display(Name = "Delivery address")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [StringLength(500), Display(Name = "Order notes")]
    public string? Notes { get; set; }

    public CartViewModel Cart { get; set; } = new();
}
