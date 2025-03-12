using BookingFoodServie2.Model.Dto;
using BookingFoodServie2.Model.Entitties;

namespace BookingFoodServie2.Service.Mapping;
using AutoMapper;
public class Mealmaping: Profile
{
    public Mealmaping()
    {
        CreateMap<Meal, MealDTO>();
        CreateMap<MealDTO, Meal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Không ánh xạ Id nếu không cần
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Giữ thời điểm tạo
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)); // Cập nhật UpdatedAt
    }
}