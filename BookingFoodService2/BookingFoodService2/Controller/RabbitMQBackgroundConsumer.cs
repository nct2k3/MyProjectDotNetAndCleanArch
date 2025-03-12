using BookingFoodServie2.Service.MessageRabiitMq;

namespace BookingFoodServie2.Controller;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class RabbitMQBackgroundConsumer : BackgroundService
{
    private readonly RabbitMQConsumer _consumer;

    public RabbitMQBackgroundConsumer()
    {
        _consumer = new RabbitMQConsumer();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Chạy Consumer để lắng nghe hàng đợi
        Task.Run(() => _consumer.StartConsuming(), stoppingToken);
        return Task.CompletedTask;
    }
}
