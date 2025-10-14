using Microsoft.EntityFrameworkCore;
using SalesService.Models;

namespace SalesService.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
    }
}