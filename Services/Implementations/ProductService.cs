using ProductManagement.Api.DTOs.Requests;
using ProductManagement.Api.DTOs.Responses;
using ProductManagement.Api.Models;
using ProductManagement.Api.Repositories.Interfaces;
using ProductManagement.Api.Services.Interfaces;

namespace ProductManagement.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IExchangeRateService _exchangeRateService;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, IExchangeRateService exchangeRateService,
        ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _exchangeRateService = exchangeRateService;
        _logger = logger;
    }

    public async Task<int> CreateAsync(CreateProductRequest request)
    {
        _logger.LogInformation("Creando producto {NombreProducto}", request.Name);

        Product product = new()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        Int32 productId = await _productRepository.CreateAsync(product);

        _logger.LogInformation("Producto creado correctamente con Id {ProductId}", productId);

        return productId;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        _logger.LogInformation("Consultando todos los productos");

        IEnumerable<Product> products =
            await _productRepository.GetAllAsync();

        IEnumerable<ProductResponse> response =
            products.Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DateCreation = product.DateCreation
            });

        return response;
    }

    public async Task<ProductResponse?> GetByIdAsync(Int32 id)
    {
        _logger.LogInformation("Consultando producto con Id {ProductId}", id);

        ProductResponse? response = null;

        Product? product =
            await _productRepository.GetByIdAsync(id);

        if (product != null)
        {
            response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DateCreation = product.DateCreation
            };
        }

        return response;
    }

    public async Task UpdateAsync(Int32 id, UpdateProductRequest request)
    {
        _logger.LogInformation("Actualizando producto con Id {ProductId}", id);

        Product product = new()
        {
            Id = id,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        await _productRepository.UpdateAsync(product);
        _logger.LogInformation("Producto con Id {ProductId} actualizado correctamente", id);
    }

    public async Task DeleteAsync(Int32 id)
    {
        _logger.LogInformation("Eliminando producto con Id {ProductId}", id);

        await _productRepository.DeleteAsync(id);

        _logger.LogInformation("Producto con Id {ProductId} eliminado correctamente", id);
    }

    public async Task<ConvertedPriceResponse?> GetConvertedPriceAsync(Int32 id)
    {
        _logger.LogInformation("Consultando precio convertido para el producto con Id {ProductId}", id);

        ConvertedPriceResponse? response = null;

        Product? product =
            await _productRepository.GetByIdAsync(id);

        if (product != null)
        {
            Decimal exchangeRate = await _exchangeRateService.GetUsdToCopRateAsync();

            response = new()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                PriceUsd = product.Price,
                ExchangeRate = exchangeRate,
                PriceCop = product.Price * exchangeRate
            };

            _logger.LogInformation("Precio convertido calculado correctamente para el producto con Id {ProductId}", id);
        }

        return response;
    }
}