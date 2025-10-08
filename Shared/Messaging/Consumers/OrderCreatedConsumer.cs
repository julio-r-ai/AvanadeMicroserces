using MassTransit;
using StockService.Messaging.Events;

namespace StockService.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation("Recebido evento de pedido criado: {OrderId}", message.OrderId);

            // TODO: atualizar o estoque conforme os produtos do pedido
            foreach (var item in message.Items)
            {
                _logger.LogInformation("Produto {ProductId} - Quantidade: {Quantity}", item.ProductId, item.Quantity);
                // Aqui entraria sua lógica de decremento no estoque
            }

            await Task.CompletedTask;
        }
    }
}