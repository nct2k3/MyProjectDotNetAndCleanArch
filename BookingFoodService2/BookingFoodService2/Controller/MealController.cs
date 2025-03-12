using BookingFoodServie2.Model.Dto;
using BookingFoodServie2.Service.Comands;
using Microsoft.AspNetCore.Mvc;

namespace BookingFoodServie2.Controller;


[ApiController]
[Route("Meal")]
public class MealController:ControllerBase
{
    private readonly MealCommand _mealCommand;

    public MealController(MealCommand mealCommand)
    {
        _mealCommand = mealCommand;
    }
    
    [HttpPost("AddMeal")]
    public async Task<IActionResult> AddMealAsync([FromBody] MealDTO meal)
    {
        MealDTO meals =await _mealCommand.CreateMealAsync(meal);
        
        return Ok(meals);
        
    }
    
    
    
}