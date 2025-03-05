using Application.Authentication.Commands.Register;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BockingFood.Controllers;

using MapsterMapper;
using MediatR;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequests request)
    {
        // Chuyển đổi request thành command
        var command = _mapper.Map<RegisterCommand>(request);

        // Gửi command qua Mediator và nhận kết quả
        var authResult = await _mediator.Send(command);

        // Trả về kết quả thành công
        return Ok(authResult);
    }

    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok(new { Message = "This is a public endpoint!" });
    }
}

[Authorize(Policy = "AdminOnly")]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    public IActionResult GetAdminDashboard()
    {
        return Ok("Chào mừng Admin đến với dashboard!");
    }
}

[Authorize(Policy = "UserOnly")]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("profile")]
    public IActionResult GetUserProfile()
    {
        return Ok("Chào mừng User đến với trang cá nhân!");
    }
}

