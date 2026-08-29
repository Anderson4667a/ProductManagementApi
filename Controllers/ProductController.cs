using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.DTOs.Requests;
using ProductManagement.Api.DTOs.Responses;
using ProductManagement.Api.Services.Interfaces;

namespace ProductManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll()
    {
        IEnumerable<ProductResponse> products =
            await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetById(Int32 id)
    {
        ProductResponse? product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Int32>> Create(CreateProductRequest request)
    {
        Int32 productId = await _productService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = productId },
            productId);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        Int32 id,
        UpdateProductRequest request)
    {
        await _productService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(Int32 id)
    {
        await _productService.DeleteAsync(id);

        return NoContent();
    }

    [HttpGet("{id:int}/price-converted")]
    public async Task<ActionResult<ConvertedPriceResponse>>GetConvertedPrice(int id)
    {
        ConvertedPriceResponse? response =
            await _productService.GetConvertedPriceAsync(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }
}
