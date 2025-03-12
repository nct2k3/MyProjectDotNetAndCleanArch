using Application.Application.Commands.Register;
using Application.Application.Common;
using Application.Application.Queries;
using Contract;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingFoodService.Controller;

[ApiController]
[Route("/BookingFood/Authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly MessageQueeu _messageQueeu;
    public AuthenticationController(ISender sender, IMapper mapper, MessageQueeu messageQueeu)
    {
        _sender = sender;
        _mapper = mapper;
        _messageQueeu = messageQueeu;
        
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequests registerRequest)
    {
        var user = _mapper.Map<RegisterCommands>(registerRequest);
        var authen = await _sender.Send(user);
        return Ok(authen);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequests loginRequest)
    {
        var user = _mapper.Map<LoginQuery>(loginRequest);
        var authen = await _sender.Send(user);
        return Ok(authen);
    }
    
}