using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models;

public class Order
{
    public int Id { get; set; }

    public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

    [Required, StringLength(120)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(180)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, StringLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, StringLength(250)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Notes { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal DeliveryFee { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Tax { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
