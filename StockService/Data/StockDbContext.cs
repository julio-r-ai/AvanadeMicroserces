using Microsoft.EntityFrameworkCore;
using StockService.Data.Entities;

namespace StockService.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}