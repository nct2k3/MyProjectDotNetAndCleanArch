namespace Contract;

public record OrderRequest(
    Guid UserId,
    DateTime OrderDate,
    string Status,
    decimal TotalAmount,
    List<DetailOrderRequest> Details
);