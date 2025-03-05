using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// Một phương án bắt lỗi dự phòng khi middleware và filters không xử lý tới
namespace BockingFood.Controllers;

    [ApiController]
    public class ErrorController: ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return Problem();
        }
        
    }
