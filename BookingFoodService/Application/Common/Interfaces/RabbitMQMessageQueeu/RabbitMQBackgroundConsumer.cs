namespace Application.Common.Interfaces.RabbitMQMessageQueeu;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

public class RabbitMQBackgroundConsumer:BackgroundService
{
    private readonly IRabbitMQComsumer _rabbitMQComsumer;

    public RabbitMQBackgroundConsumer(IRabbitMQComsumer rabbitMQComsumer)
    {
        _rabbitMQComsumer = rabbitMQComsumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(()=>_rabbitMQComsumer.StartConsuming("queue"));
        return Task.CompletedTask;
        
    }
}