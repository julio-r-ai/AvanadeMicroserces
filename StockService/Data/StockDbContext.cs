using Microsoft.EntityFrameworkCore;
using StockService.Models;

namespace StockService.Data;

public class StockDbContext : DbContext
{
    public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}