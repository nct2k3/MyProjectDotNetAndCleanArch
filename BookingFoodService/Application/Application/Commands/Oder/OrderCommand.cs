using Application.Application.Common;
using MediatR;

namespace Application.Application.Commands.Oder;

public record OrderCommand(
    Guid UserId,
    DateTime OrderDate,
    string Status,
    decimal TotalAmount,
    List<DetailOrderCommand> Details
) : IRequest<OrderResult>;