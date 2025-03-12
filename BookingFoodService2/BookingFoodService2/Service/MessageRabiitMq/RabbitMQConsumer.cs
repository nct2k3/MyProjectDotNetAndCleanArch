namespace BookingFoodServie2.Service.MessageRabiitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

public class RabbitMQConsumer
{
    private readonly string _hostName = "localhost"; 
    private readonly string _queueName = "nt";      
    private readonly string _userName = "guest";  
    private readonly string _password = "guest";  

    public void StartConsuming()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostName,
            Port = 5672,
            UserName = _userName,
            Password = _password
        };

        // Tạo kết nối đến RabbitMQ
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Đảm bảo hàng đợi tồn tại (không tạo lại nếu đã tồn tại)
        channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        Console.WriteLine($"[*] Waiting for messages in queue: {_queueName}");

        // Tạo Consumer
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[x] Received message: {message}");

            // Thực hiện xử lý logic tùy ý
            ProcessMessage(message);

            // Gửi ACK sau khi xử lý xong
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        // Đăng ký consumer lắng nghe hàng đợi
        channel.BasicConsume(
            queue: _queueName,
            autoAck: false, // Đặt false để kiểm soát ACK thủ công
            consumer: consumer);

        // Giữ chương trình chạy để lắng nghe liên tục
        Console.WriteLine("Press [Enter] to exit.");
        Console.ReadLine();
    }

    private void ProcessMessage(string message)
    {
        // Thêm logic xử lý tin nhắn tại đây
        Console.WriteLine($"Processing message: {message}");
    }
}
