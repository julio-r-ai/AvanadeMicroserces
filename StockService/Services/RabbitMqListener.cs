using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace StockService.Services
{
    public class RabbitMqListener
    {
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqListener(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync()
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "rabbitmq" // ou "localhost", dependendo do seu ambiente
            };

            // Cria conexão e canal
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "sales",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);

            // ✅ Correção da assinatura do evento: agora com (sender, ea)
            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"📨 Mensagem recebida: {message}");

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "sales",
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("✅ RabbitMQ listener iniciado e aguardando mensagens...");
        }
    }
}