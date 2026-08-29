using Dapper;
using ProductManagement.Api.Data;
using ProductManagement.Api.Models;
using ProductManagement.Api.Repositories.Interfaces;
using System.Data;

namespace ProductManagement.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DapperContext _context;

    public ProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<Int32> CreateAsync(Product product)
    {
        using IDbConnection connection = _context.CreateConnection();

        object parameters = new
        {
            product.Name,
            product.Description,
            product.Price
        };

        Int32 productId = await connection.QuerySingleAsync<Int32>(
            "sp_Product_Create",
            parameters,
            commandType: CommandType.StoredProcedure);

        return productId;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        using IDbConnection connection = _context.CreateConnection();

        IEnumerable<Product> products =
            await connection.QueryAsync<Product>(
                "sp_Product_GetAll",
                commandType: CommandType.StoredProcedure);

        return products;
    }

    public async Task<Product?> GetByIdAsync(Int32 id)
    {
        using IDbConnection connection = _context.CreateConnection();

        Product? product =
            await connection.QueryFirstOrDefaultAsync<Product>(
                "sp_Product_GetById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);

        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        using IDbConnection connection = _context.CreateConnection();

        object parameters = new
        {
            product.Id,
            product.Name,
            product.Description,
            product.Price
        };

        await connection.ExecuteAsync(
            "sp_Product_Update",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(Int32 id)
    {
        using IDbConnection connection = _context.CreateConnection();

        object parameters = new
        {
            Id = id
        };

        await connection.ExecuteAsync(
            "sp_Product_Delete",
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}