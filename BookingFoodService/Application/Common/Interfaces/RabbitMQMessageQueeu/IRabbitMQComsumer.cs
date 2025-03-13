namespace Application.Common.Interfaces.RabbitMQMessageQueeu;

public interface IRabbitMQComsumer
{
    void StartConsuming(string queueName);
    void ProcessMessage(string message);
    
}