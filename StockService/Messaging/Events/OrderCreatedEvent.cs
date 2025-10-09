namespace StockService.Messaging.Events
{
    using System;
    using System.Collections.Generic;

    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
