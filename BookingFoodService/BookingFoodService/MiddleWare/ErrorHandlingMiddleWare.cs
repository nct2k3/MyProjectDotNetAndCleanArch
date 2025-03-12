using System.Text.Json;
using BookingFoodService.Common.Error;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookingFoodService.MiddleWare;

public class ErrorHandlingMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ProblemDetailsFactory _myProblemDetailFactory;

    public ErrorHandlingMiddleWare(RequestDelegate next, ProblemDetailsFactory myProblemDetailFactory)
    {
        _next = next;
        _myProblemDetailFactory = myProblemDetailFactory;
        
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
            if (context.Response.StatusCode== StatusCodes.Status401Unauthorized)
            {
                await  HandleStatusCodeAsync(context, StatusCodes.Status401Unauthorized,"UnAuthorrized", _myProblemDetailFactory);
            }
            else
            if (context.Response.StatusCode== StatusCodes.Status403Forbidden)
            {
                await HandleStatusCodeAsync(context, StatusCodes.Status403Forbidden, "Forbidden", _myProblemDetailFactory);
                
            }
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e, _myProblemDetailFactory);
        }
        
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ProblemDetailsFactory problemDetailsFactory)
    {
        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Internal Server Error",
            detail: exception.Message,
            instance: context.Request.Path
        );

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }

    private static Task HandleStatusCodeAsync(HttpContext context, int statusCode, string title, ProblemDetailsFactory problemDetailsFactory)
    {
        var problemDetails = problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode: statusCode,
            title: title,
            detail: statusCode == StatusCodes.Status401Unauthorized
                ? "You are not authenticated. Please log in."
                : "You do not have permission to access this resource.",
            instance: context.Request.Path
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}