using Domain.Entities;

namespace Application.Application.Common;

public record OrderResult
{
    public Order Order { get; init; } = null!;
    public List<OrderDetail> OrderDetails { get; init; } = new();
}