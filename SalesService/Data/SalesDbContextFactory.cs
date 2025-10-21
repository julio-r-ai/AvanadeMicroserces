using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SalesService.Data
{
    public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
    {
        public SalesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();

            // 🔧 Ajuste aqui se o nome do seu banco for outro
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=SalesDb;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return new SalesDbContext(optionsBuilder.Options);
        }
    }
}