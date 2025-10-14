namespace Shared.Events
{
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}