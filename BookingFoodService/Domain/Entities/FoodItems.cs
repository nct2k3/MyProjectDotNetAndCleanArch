using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class FoodItems
{
    [Key]public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string Category { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsAvailable { get; set; }
}