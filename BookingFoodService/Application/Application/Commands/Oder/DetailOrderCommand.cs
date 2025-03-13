namespace Application.Application.Commands.Oder;

public record DetailOrderCommand(
    Guid FoodId,
    int Quantity,
    decimal Price
);