using System.Text.Json;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Nếu không có validator, tiếp tục xử lý tiếp theo
        if (_validator == null)
        {
            return await next();
        }

        // Thực hiện validate
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }

        // Xử lý lỗi validate
        var errors = validationResult.Errors.Select(failure => new
        {
            Property = failure.PropertyName,
            Message = failure.ErrorMessage
        }).ToList();

        var errorDetail = JsonSerializer.Serialize(errors);

        // Ném ValidationException với chi tiết lỗi
        throw new ValidationException(errorDetail);
    }
}