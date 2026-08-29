using ProductManagement.Api.DTOs.Requests;
using ProductManagement.Api.DTOs.Responses;

namespace ProductManagement.Api.Services.Interfaces;

public interface IProductService
{
    Task<Int32> CreateAsync(CreateProductRequest request);

    Task<IEnumerable<ProductResponse>> GetAllAsync();

    Task<ProductResponse?> GetByIdAsync(Int32 id);

    Task UpdateAsync(Int32 id, UpdateProductRequest request);

    Task DeleteAsync(Int32 id);

    Task<ConvertedPriceResponse?> GetConvertedPriceAsync(int id);
}
