using Application.Application.Queries;

namespace Application.Common.Interfaces.RabbitMQMessageQueeu;

public interface IRabbitMqMessageQueeu
{
    void SendMessageQueeu(string queueName, string message);
    string GetMessageQueeu(string queueName);
    
}