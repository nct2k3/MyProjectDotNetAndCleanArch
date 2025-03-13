using FoodBillingService.Service.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodBillingService.Controller;

public class RabbitMQBackgroundConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RabbitMQBackgroundConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var consumer = scope.ServiceProvider.GetRequiredService<OrderConsumer>();
                    consumer.StartConsuming("Invoices");
                }

                // Thêm delay nhỏ nếu cần để tránh vòng lặp quá nhanh
                Task.Delay(1000, stoppingToken).Wait(stoppingToken);
            }
        }, stoppingToken);

        return Task.CompletedTask;
    }
}