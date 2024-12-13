using Cart.Shared.DTOs.Product;
using Cart.Shared.Features;

namespace Cart.Service.Contracts.Interfaces;

public interface IProductService
{
    public Task DeleteProductByIdAsync(Guid Id);
    public Task<ProductDto> GetProductByIdAsync(Guid Id);
    public Task<ProductDto> CreateProductAsync(ProductToAddDto product, string userId);
    public Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductParameters parameters);
}