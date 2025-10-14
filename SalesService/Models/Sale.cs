namespace SalesService.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<SaleItem> Items { get; set; } = new();
    }

    public class SaleItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}