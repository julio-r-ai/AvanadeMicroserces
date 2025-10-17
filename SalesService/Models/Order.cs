namespace SalesService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending"; // ✅ novo
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // ✅ novo
        // Relacionamento com os itens
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
