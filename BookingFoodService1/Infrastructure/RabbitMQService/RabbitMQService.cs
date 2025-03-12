using System.Text;
using Presentation.Common.Interfaces.MessageQueueService;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQService
{
    public class RabbitMQService : IMessageQueueService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQService()
        {
            // Cấu hình RabbitMQ
            _factory = new ConnectionFactory
            {
                HostName = "localhost", // Địa chỉ RabbitMQ
                Port = 5672, 
                UserName = "guest",    // Tên đăng nhập mặc định
                Password = "guest"     // Mật khẩu mặc định
            };
        }

        // Gửi tin nhắn vào RabbitMQ
        public  void SendMessage(string queueName, string message)
        {
            // Sử dụng await để lấy kết nối bất đồng bộ
            using var connection =  _factory.CreateConnection();
            using var channel = connection.CreateModel(); // Sửa lỗi CreateModel

            // Tạo hàng đợi nếu chưa tồn tại
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            // Gửi tin nhắn vào hàng đợi
            channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body);
        }

   
        public string ReceiveMessage(string queueName)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel(); // Sửa lỗi CreateModel

            // Đảm bảo hàng đợi tồn tại
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Nhận tin nhắn từ hàng đợi
            var result = channel.BasicGet(queue: queueName, autoAck: true);
            if (result == null)
            {
                return "No messages in queue.";
            }

            // Chuyển dữ liệu tin nhắn sang string
            return Encoding.UTF8.GetString(result.Body.ToArray());
        }
    }
}
