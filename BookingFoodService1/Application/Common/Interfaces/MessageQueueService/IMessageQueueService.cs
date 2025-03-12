using System.Collections.Concurrent;

namespace Presentation.Common.Interfaces.MessageQueueService;

public interface IMessageQueueService
{
    void SendMessage(string queueName, string message);
    string ReceiveMessage(string queueName);
}