using MassTransit;
using Microsoft.EntityFrameworkCore;
using StockService.Data;
using System.Threading.Tasks;
using StockService.Messaging.Events;
using Shared.Messaging.Events;

namespace StockService.Messaging.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly StockDbContext _db;
    private readonly IPublishEndpoint _publish;

    public OrderCreatedConsumer(StockDbContext db, IPublishEndpoint publish)
    {
        _db = db;
        _publish = publish;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var evt = context.Message;

        // Verificar estoque para todos os itens
        foreach (var item in evt.Items)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
            if (product == null || product.Quantity < item.Quantity)
            {
                await _publish.Publish(new OrderRejectedEvent(evt.OrderId, $"Produto {item.ProductId} sem estoque."));
                return;
            }
        }

        // Reduzir estoque
        foreach (var item in evt.Items)
        {
            var product = await _db.Products.FirstAsync(p => p.Id == item.ProductId);
            product.Quantity -= item.Quantity;
        }

        await _db.SaveChangesAsync();

        await _publish.Publish(new OrderConfirmedEvent(evt.OrderId));
    }
}