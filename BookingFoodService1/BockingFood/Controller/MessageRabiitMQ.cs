using Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.Authentication.Comands.MessageRabbitMQService;
using Presentation.Common.Interfaces.MessageQueueService;

namespace BockingFood.Controllers;

[ApiController]
[Route("message")]
public class MessageRabiitMQ:ControllerBase
{
    private readonly MessageRabbitMQService _messageRabbitMqService;
    
    public MessageRabiitMQ(MessageRabbitMQService messageQueueService)
    {
        _messageRabbitMqService = messageQueueService;
            
    }

    [HttpPost("message")]
    public async Task<IActionResult> RegisterAsync(string queueName, string message)
    {
        try
        {
            _messageRabbitMqService.Send(queueName, message);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
    [HttpPost("Takemessage")]
    public async Task<IActionResult> TakeMessgaeAsync(string queueName)
    {
        try
        {
           string message = _messageRabbitMqService.TakeMessage(queueName);
            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
    
}