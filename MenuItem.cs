using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderingSystem.Models;

public class MenuItem
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(400)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 9999)]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [StringLength(250)]
    public string ImageUrl { get; set; } = "/images/menu/placeholder.jpg";

    public bool IsAvailable { get; set; } = true;

    public bool IsFeatured { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
