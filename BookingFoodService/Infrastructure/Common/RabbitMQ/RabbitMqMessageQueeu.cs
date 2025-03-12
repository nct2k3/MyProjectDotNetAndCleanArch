using System.Text;
using Application.Common.Interfaces.RabbitMQMessageQueeu;
using Microsoft.IdentityModel.Protocols;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common.RabbitMQ;

public class RabbitMqMessageQueeu:IRabbitMqMessageQueeu
{
    private readonly ConnectionFactory _factory;

    public RabbitMqMessageQueeu(IConfiguration configuration)
    {
        _factory = new ConnectionFactory
        {
            HostName = configuration.GetConnectionString("HostName"),
            Port = int.TryParse(configuration.GetConnectionString("Port"), out var port) ? port : 5672, // Default RabbitMQ port
            UserName = configuration.GetConnectionString("UserName"),
            Password = configuration.GetConnectionString("Password"),
        };
    }

    
    public void SendMessageQueeu(string queueName, string message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        var body= Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(
            exchange:"",
            routingKey: queueName,
            basicProperties: null,
            body: body
            );
        
    }

    public string GetMessageQueeu(string queueName)
    {
       using var connection = _factory.CreateConnection();
       using var channel = connection.CreateModel();
       channel.QueueDeclare(
           queue: queueName,
           durable: false,
           exclusive: false,
           autoDelete: false,
           arguments: null);
       var result = channel.BasicGet(queue: queueName, autoAck: true);
       if (result == null)
       {
           return null;
           
       }
       return Encoding.UTF8.GetString(result.Body.ToArray());
    }
}