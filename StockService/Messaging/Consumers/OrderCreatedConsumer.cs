using System.Threading.Tasks;
using MassTransit;
using StockService.Data.Entities;
using StockService.Repositories;
using Shared.Events; // Certifique-se de que o namespace do OrderCreatedEvent está correto
using Shared.Messages; // 👈 importante!

namespace StockService.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            var order = context.Message;

            Console.WriteLine($"Pedido recebido: {order.Id}");

            // lógica de estoque aqui

            await Task.CompletedTask;
        }
    }
}