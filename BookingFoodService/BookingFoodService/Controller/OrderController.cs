using Application.Application.Commands.Oder;
using Contract;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingFoodService.Controller;

[ApiController]
[Route("Order")]
public class OrderController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public OrderController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] OrderRequest orderRequest)
    {
        var orderCommand = _mapper.Map<OrderCommand>(orderRequest);
        var result = await _sender.Send(orderCommand);
        return Ok(result);
    }
}