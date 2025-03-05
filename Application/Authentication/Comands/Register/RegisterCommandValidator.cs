using Application.Authentication.Commands.Register;
using FluentValidation;

namespace Presentation.Authentication.Comands.Register;

public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
{
    
    // cau hinh yeu cau du lieu can co vi du khac rong, email dang email.com
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters.");
    }
    
}