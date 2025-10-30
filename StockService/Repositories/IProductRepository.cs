using StockService.Data.Entities;

namespace StockService.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
    }
}