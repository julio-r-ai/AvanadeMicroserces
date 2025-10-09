using System.Threading.Tasks;
using MassTransit;
using StockService.Data.Entities;
using StockService.Repositories;
using Shared.Events; // Certifique-se de que o namespace do OrderCreatedEvent está correto

namespace StockService.Messaging.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IProductRepository _productRepository;

        public OrderCreatedConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var orderEvent = context.Message;

            foreach (var orderItem in orderEvent.Items) // loop sobre os itens do pedido
            {
                // Busca o produto pelo Id
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    // Atualiza a quantidade em estoque
                    product.Quantity -= orderItem.Quantity;

                    // Salva a alteração
                    await _productRepository.UpdateAsync(product);
                }
            }
        }
    }
}