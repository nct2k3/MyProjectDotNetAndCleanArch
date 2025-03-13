using System.Text;
using Application.Common.Interfaces.RabbitMQMessageQueeu;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Common.RabbitMQ;

public class RabbitMQComsumer : IRabbitMQComsumer
{
    private readonly ConnectionFactory _factory;

    public RabbitMQComsumer(IConfiguration configuration)
    {
        _factory = new ConnectionFactory
        {
            HostName = configuration.GetConnectionString("HostName"),
            Port = int.TryParse(configuration.GetConnectionString("Port"), out var port)
                ? port
                : 5672, // Default RabbitMQ port
            UserName = configuration.GetConnectionString("UserName"),
            Password = configuration.GetConnectionString("Password"),
        };
    }
    public void StartConsuming(string queueName)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete:false,
            arguments: null

        );
        Console.WriteLine($"[*] Waiting for messages in queue: {queueName}");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            ProcessMessage(message);
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        
        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }

    public void ProcessMessage(string message)
    {
        Console.WriteLine($"My Message :{message} ");
    }

}