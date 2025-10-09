using StockService.Data.Entities;
using System.Threading.Tasks;

namespace StockService.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task UpdateAsync(Product product);
    }
}