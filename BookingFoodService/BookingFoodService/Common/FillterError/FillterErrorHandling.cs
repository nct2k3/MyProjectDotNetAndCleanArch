using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookingFoodService.Common.FillterError;

public class FillterErrorHandling:ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var statusCode = exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };
        var prb = new ProblemDetails
        {
            Type = $"https://httpstatuses.com/{(int)statusCode}",
            Title = exception.Message,
            Detail = exception.InnerException?.Message ?? "An error occurred while processing your request",
            Status = (int)statusCode,
            Instance = context.HttpContext.Request.Path
        };
        prb.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
        prb.Extensions["customInfo"] ="I can't do everithing when you have error" ;
        context.Result = new ObjectResult(prb)
        {
            StatusCode = (int)statusCode
        };
        context.ExceptionHandled = true;

    }
} 