using Presentation.Common.Interfaces.MessageQueueService;
using Presentation.Entities;

namespace Presentation.Authentication.Comands.MessageRabbitMQService;

public class MessageRabbitMQService
{
    private IMessageQueueService _messageQueueService;

    public MessageRabbitMQService(IMessageQueueService messageQueueService)
    {
        _messageQueueService = messageQueueService;
    }

    public void Send(string queueName, string message)
    {
        _messageQueueService.SendMessage(queueName, message);
    }
    public string TakeMessage(string queueName)
    {
      return  _messageQueueService.ReceiveMessage(queueName);
    }
    
    
}