using Application.Authentication.Common;
using MediatR;

namespace Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Role
) : IRequest<AuthenticationResult>;