using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SalesService.Data
{
    public class SalesContextFactory : IDesignTimeDbContextFactory<SalesContext>
    {
        public SalesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesContext>();

            // Connection string do seu appsettings.json
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=SalesDB;Trusted_Connection=True;MultipleActiveResultSets=true"
            );

            return new SalesContext(optionsBuilder.Options);
        }
    }
}