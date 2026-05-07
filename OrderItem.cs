using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public int MenuItemId { get; set; }

    public MenuItem? MenuItem { get; set; }

    [Required, StringLength(120)]
    public string ItemName { get; set; } = string.Empty;

    [Range(1, 99)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal LineTotal { get; set; }
}
