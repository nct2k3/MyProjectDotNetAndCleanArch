using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class OrderDetail
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid FoodId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;
    [ForeignKey(nameof(FoodId))]
    public FoodItems FoodItems { get; set; } = null!;
}