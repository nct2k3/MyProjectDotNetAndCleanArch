namespace Contract;

public record DetailOrderRequest(
    Guid FoodId,
    int Quantity,
    decimal Price
);