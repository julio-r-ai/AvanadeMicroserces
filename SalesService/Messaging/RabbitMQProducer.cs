using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace SalesService.Messaging
{
    public class RabbitMQProducer
    {
        private readonly string _hostname;
        private readonly int _port;

        public RabbitMQProducer(IConfiguration configuration)
        {
            _hostname = configuration["RabbitMQ:Host"] ?? "rabbitmq";
            _port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672");
        }

        public void SendMessage(object message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                Port = _port
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "sales_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: "sales_queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}