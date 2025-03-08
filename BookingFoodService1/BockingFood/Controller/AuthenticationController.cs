using Application.Authentication.Commands.Register;
using Contracts;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BockingFood.Controllers;

using MapsterMapper;
using MediatR;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private ApplicationDbContext _dbContext;

    public AuthenticationController(ISender mediator, IMapper mapper, ApplicationDbContext dbContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequests request)
    {
        // Chuyển đổi request thành command
        var command = _mapper.Map<RegisterCommand>(request);

        // Gửi command qua Mediator và nhận kết quả viêc dưa va ong giup xu ly cac phan ngoai le 
        var authResult = await _mediator.Send(command);

        // Trả về kết quả thành công
        return Ok(authResult);
    }

    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok(new { Message = "This is a public endpoint!" });
    }
    
    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync();
            return Ok("Database connection is working!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Connection failed: {ex.Message}");
        }
    }

}

