public record OrderItemDto(Guid ProductId, int Quantity);
public record OrderCreatedEvent(Guid OrderId, List<OrderItemDto> Items, string CustomerName, DateTime CreatedAt);
public record OrderConfirmedEvent(Guid OrderId);
public record OrderRejectedEvent(Guid OrderId, string Reason);