using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Models;

namespace RestaurantOrderingSystem.Data;

public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Starters", Description = "Small plates to begin the meal." },
            new Category { Id = 2, Name = "Mains", Description = "Signature plates and hearty bowls." },
            new Category { Id = 3, Name = "Desserts", Description = "Sweet finishes made in-house." },
            new Category { Id = 4, Name = "Drinks", Description = "Fresh coolers, coffee, and tea." });

        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, CategoryId = 1, Name = "Crispy Paneer Bites", Description = "Golden paneer cubes with mint yogurt dip.", Price = 229, ImageUrl = "/images/menu/paneer-bites.jpg", IsFeatured = true },
            new MenuItem { Id = 2, CategoryId = 1, Name = "Garlic Herb Fries", Description = "Skin-on fries tossed with garlic butter and parsley.", Price = 149, ImageUrl = "/images/menu/fries.jpg" },
            new MenuItem { Id = 3, CategoryId = 2, Name = "Tandoori Chicken Bowl", Description = "Smoky chicken, saffron rice, salad, and house chutney.", Price = 389, ImageUrl = "/images/menu/chicken-bowl.jpg", IsFeatured = true },
            new MenuItem { Id = 4, CategoryId = 2, Name = "Veggie Supreme Pizza", Description = "Thin crust pizza with peppers, olives, corn, and mozzarella.", Price = 449, ImageUrl = "/images/menu/pizza.jpg" },
            new MenuItem { Id = 5, CategoryId = 2, Name = "Creamy Mushroom Pasta", Description = "Penne in parmesan cream sauce with sauteed mushrooms.", Price = 349, ImageUrl = "/images/menu/pasta.jpg" },
            new MenuItem { Id = 6, CategoryId = 3, Name = "Chocolate Lava Cake", Description = "Warm chocolate cake with vanilla ice cream.", Price = 199, ImageUrl = "/images/menu/lava-cake.jpg", IsFeatured = true },
            new MenuItem { Id = 7, CategoryId = 4, Name = "Mint Lime Cooler", Description = "Fresh lime, mint, soda, and crushed ice.", Price = 119, ImageUrl = "/images/menu/cooler.jpg" },
            new MenuItem { Id = 8, CategoryId = 4, Name = "Cold Coffee", Description = "Chilled coffee blended with milk and a light cream top.", Price = 159, ImageUrl = "/images/menu/cold-coffee.jpg" });
    }
}
