using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BockingFood.Filters
{
    public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
    {
        // Override phương thức để xử lý lỗi và trả về ProblemDetails
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Xác định mã trạng thái HTTP dựa trên loại lỗi
            var statusCode = exception switch
            {
                ArgumentNullException => HttpStatusCode.BadRequest, // Lỗi tham số không hợp lệ
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // Không được phép truy cập
                _ => HttpStatusCode.InternalServerError // Lỗi máy chủ
            };

            // Tạo ProblemDetails theo chuẩn RFC 7807
            var problemDetails = new ProblemDetails
            {
                Type = $"https://httpstatuses.com/{(int)statusCode}",
                Title = exception.Message,
                Detail = exception.InnerException?.Message ?? "An error occurred while processing your request.",
                Status = (int)statusCode,
                Instance = context.HttpContext.Request.Path
            };

            // Gắn thêm thông tin tùy chỉnh nếu cần
            problemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
            problemDetails.Extensions["customInfo"] = "Additional details if needed";

            // Đặt kết quả trả về
            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = (int)statusCode
            };

            context.ExceptionHandled = true; // Đánh dấu lỗi đã được xử lý
        }
    }
}