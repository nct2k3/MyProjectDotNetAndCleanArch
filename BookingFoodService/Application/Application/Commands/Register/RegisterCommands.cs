using Application.Application.Common;
using MediatR;

namespace Application.Application.Commands.Register;

public record RegisterCommands
(
    string FirstName,
    string LastName,
    string Email,
    int PhoneNumber ,
    string Address,
    string Password,
    string Role
    ):IRequest<AuthenticationResult>;