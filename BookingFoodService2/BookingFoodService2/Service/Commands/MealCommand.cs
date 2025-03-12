using AutoMapper;
using BookingFoodServie2.Controller;
using BookingFoodServie2.Model.Dto;
using BookingFoodServie2.Model.Entitties;

namespace BookingFoodServie2.Service.Comands;

public class MealCommand
{
    
    public IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MealCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<MealDTO> CreateMealAsync(MealDTO mealDto)
    {

        var meal = _mapper.Map<Meal>(mealDto);
        
        await _unitOfWork.Repository<Meal>().AddAsync(meal);

        return _mapper.Map<MealDTO>(meal);
    }
    
}