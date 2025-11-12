using System;
using System.Collections.Generic;

namespace SalesService.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public List<OrderItem> Items { get; set; } = new();
    }
}