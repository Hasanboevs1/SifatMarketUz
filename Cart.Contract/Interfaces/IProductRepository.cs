using Cart.Entities.Models;
using Cart.Shared.Features;

namespace Cart.Contract.Interfaces;

public interface IProductRepository : IRepositoryBase<Product>
{
    public void AddProduct(Product product);
    public void DeleteProduct(Product product);
    public Task<Product> GetProductByIdAsync(Guid Id);
    public Task<IEnumerable<Product>> GetAllProductsAsync(ProductParameters parameters);
}