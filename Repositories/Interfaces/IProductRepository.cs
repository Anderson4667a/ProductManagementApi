using ProductManagement.Api.Models;

namespace ProductManagement.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Int32> CreateAsync(Product product);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Int32 id);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Int32 id);
}