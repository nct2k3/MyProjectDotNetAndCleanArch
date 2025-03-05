using System.Text.Json;

namespace Presentation.Common.Behaviors;
using FluentValidation;
using MediatR;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return await next(); // Nếu không có validator, tiếp tục luồng xử lý.
        }

        // Kiểm tra tính hợp lệ của dữ liệu.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }


        var errors = validationResult.Errors
            .Select(failure => new 
            { 
                Property = failure.PropertyName, 
                Message = failure.ErrorMessage 
            })
            .ToList();
        var errorDetails = JsonSerializer.Serialize(errors);

        throw new Exception(errorDetails);
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        throw new NotImplementedException();
    }
}
