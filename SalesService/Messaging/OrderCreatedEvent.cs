using SalesService.DTOs;
using SalesService.Messaging;


namespace SalesService.Messaging
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }

        public OrderCreatedEvent(int orderId, List<OrderItemDto> items, string customerName, DateTime createdAt)
        {
            OrderId = orderId;
            Items = items;
            CustomerName = customerName;
            CreatedAt = createdAt;
        }
    }
}