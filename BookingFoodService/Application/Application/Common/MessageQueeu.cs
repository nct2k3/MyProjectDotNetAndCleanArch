using Application.Common.Interfaces.RabbitMQMessageQueeu;

namespace Application.Application.Common;

public class MessageQueeu
{
    private readonly IRabbitMqMessageQueeu _rabbitMqMessageQueeu;

    public MessageQueeu(IRabbitMqMessageQueeu rabbitMqMessageQueeu)
    {
        _rabbitMqMessageQueeu = rabbitMqMessageQueeu;
    }

    public void SendMessage(string queueName, string message)
    {
        _rabbitMqMessageQueeu.SendMessageQueeu(queueName, message);
    }
}