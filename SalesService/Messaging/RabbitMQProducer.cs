using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace SalesService.Messaging
{
    public class RabbitMQProducer
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQProducer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
        }

        public void SendMessage<T>(T message, string queueName)
        {
            // 🔧 Compatível com RabbitMQ.Client 7.1.2
            using var connection = _factory.CreateConnection(_factory.ClientProvidedName ?? "SalesServiceConnection");
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}