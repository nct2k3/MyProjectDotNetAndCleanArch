using FluentValidation;

namespace Application.Application.Commands.Oder
{
    public class OrderCommandValidator : AbstractValidator<OrderCommand>
    {
        public OrderCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("OrderDate is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("TotalAmount must be greater than 0.");
            RuleForEach(x => x.Details).SetValidator(new DetailOrderCommandValidator());
        }
    }

    public class DetailOrderCommandValidator : AbstractValidator<DetailOrderCommand>
    {
        public DetailOrderCommandValidator()
        {
            RuleFor(x => x.FoodId).NotEmpty().WithMessage("FoodId is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}