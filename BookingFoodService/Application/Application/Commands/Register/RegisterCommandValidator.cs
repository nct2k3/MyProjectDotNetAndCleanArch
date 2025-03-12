using FluentValidation;

namespace Application.Application.Commands.Register;

public class RegisterCommandValidator:AbstractValidator<RegisterCommands>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters.");
    }
}