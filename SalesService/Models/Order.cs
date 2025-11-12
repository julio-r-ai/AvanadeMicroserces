using System;
using System.Collections.Generic;

namespace SalesService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public decimal Total { get; set; }

        // 🧩 Propriedades que faltavam
        public string Status { get; set; } = "Pendente";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OrderItem> Items { get; set; } = new();
    }
}