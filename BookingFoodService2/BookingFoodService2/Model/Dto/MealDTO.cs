namespace BookingFoodServie2.Model.Dto;

public class MealDTO
{
    public string Name { get; set; } = null!;
  
    public decimal Price { get; set; }

 
    public string Description { get; set; }


    public string Category { get; set; }

   
    public bool IsAvailable { get; set; } = true;

  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
    
}