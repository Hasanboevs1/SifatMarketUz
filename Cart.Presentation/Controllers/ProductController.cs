using Cart.Presentation.Filters;
using Cart.Service.Contracts.Interfaces;
using Cart.Shared.DTOs.Product;
using Cart.Shared.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Presentation.Controllers;

[Route("api/products")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ProductController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet(Name = "GetAll")]
    [ServiceFilter(typeof(ValidationProductAttribute))]
    public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameters parameters)
    {
        var products = await _serviceManager.ProductService.GetAllProductsAsync(parameters);
        return Ok(products);
    }

    [HttpGet("{id:guid}", Name = "GetById")]
    public async Task<IActionResult> GetProductById(Guid Id)
    {
        var product = await _serviceManager.ProductService.GetProductByIdAsync(Id);
        return Ok(product);
    }

    [HttpPost("add")]
    [ServiceFilter(typeof(ValidationActionAttribute))]
    public async Task<IActionResult> AddProduct([FromBody] ProductToAddDto product)
    {
        var userId = User.Claims.FirstOrDefault()?.Value;
        var _product = await _serviceManager.ProductService.CreateProductAsync(product, userId);
        return Ok(_product);
    }

    [HttpDelete("{id:guid}", Name = "DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(Guid Id)
    {
        await _serviceManager.ProductService.DeleteProductByIdAsync(Id);
        return Ok();
    }

}
