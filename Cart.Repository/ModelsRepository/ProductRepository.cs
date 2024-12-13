using Cart.Contract.Interfaces;
using Cart.Entities.Models;
using Cart.Repository.Base;
using Cart.Repository.Contexts;
using Cart.Shared.Features;
using Microsoft.EntityFrameworkCore;

namespace Cart.Repository.ModelsRepository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext context)
        : base(context) { }

    public void AddProduct(Product product) => Create(product);
    public void DeleteProduct(Product product) => base.Delete(product);
    public async Task<IEnumerable<Product>> GetAllProductsAsync(ProductParameters parameters) =>
                await FindAll()
                .OrderBy(p => p.Name)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToArrayAsync();
    public async Task<Product> GetProductByIdAsync(Guid Id) =>
                await FindByCondition(p => p.Id == Id)
                .Include(e => e.User)
                .SingleOrDefaultAsync();
}