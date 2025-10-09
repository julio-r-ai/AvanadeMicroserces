using System;

namespace StockService.Models
{
    public class Product
    {
        public Guid Id { get; set; }       // usado para retorno e criação
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}