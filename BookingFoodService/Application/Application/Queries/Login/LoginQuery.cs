using Application.Application.Common;
using MediatR;

namespace Application.Application.Queries;

public record LoginQuery
(
    string Email,
    string Password
) : IRequest<AuthenticationResult>;