using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using FoodBillingService.Infrastructure;
using FoodBillingService.Model.Entities;

namespace FoodBillingService.Service.Common;

public class OrderConsumer
{
    private readonly ConnectionFactory _factory;
    private readonly IUnitOfWork _unitOfWork;

    public OrderConsumer(IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _factory = new ConnectionFactory
        {
            HostName = configuration.GetConnectionString("HostName"),
            Port = int.TryParse(configuration.GetConnectionString("Port"), out var port) ? port : 5672, // Default RabbitMQ port
            UserName = configuration.GetConnectionString("UserName"),
            Password = configuration.GetConnectionString("Password"),
        };
       _unitOfWork = unitOfWork;
    }

    public void StartConsuming(string queueName)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received JSON: {message}");
            Order order = JsonSerializer.Deserialize<Order>(message);
            Console.WriteLine($"OrderId: {order.OrderId}");
            ProcessOrder(order);
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        channel.BasicConsume(
            queue: queueName,
            autoAck: false, 
            consumer: consumer);
        
        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }

    private async void ProcessOrder(Order order)
    {
        try
        {
            var repository =_unitOfWork.Repository<Order>();
            Console.WriteLine($"OrderId: {order.OrderId}, User: {order.User.FirstName} {order.User.LastName}");
            await repository.SaveOrderAsync(order);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error processing order: {e}");
            throw;
        }
    }

}