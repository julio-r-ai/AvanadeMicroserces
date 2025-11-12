using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SalesService.Data
{
    public class SalesContextFactory : IDesignTimeDbContextFactory<SalesContext>
    {
        public SalesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost,1434;Database=SalesDb;User Id=sa;Password=Julio@21#;TrustServerCertificate=True;"
            );

            return new SalesContext(optionsBuilder.Options);
        }
    }
}