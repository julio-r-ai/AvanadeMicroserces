using SalesService.Data;
using SalesService.Models;
using SalesService.Messaging;

namespace SalesService.Services
{
    public class SalesManager
    {
        private readonly SalesContext _context;
        private readonly RabbitMQProducer _producer;

        public SalesManager(SalesContext context, RabbitMQProducer producer)
        {
            _context = context;
            _producer = producer;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            // Simulação: em um cenário real, chamaríamos o StockService para validar estoque.
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            // Notifica o RabbitMQ
            _producer.SendMessage(new
            {
                SaleId = sale.Id,
                Items = sale.Items.Select(i => new { i.ProductId, i.Quantity })
            }, "sales_created_queue");

            return sale;
        }

        public IEnumerable<Sale> GetSales()
        {
            return _context.Sales.ToList();
        }
    }
}