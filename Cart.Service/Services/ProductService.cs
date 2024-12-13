using AutoMapper;
using Cart.Contract.Interfaces;
using Cart.Entities.ErrorModels;
using Cart.Entities.Models;
using Cart.Service.Contracts.Interfaces;
using Cart.Shared.DTOs.Product;
using Cart.Shared.Features;

namespace Cart.Service.Services;

public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateProductAsync(ProductToAddDto productToAdd, string userId)
    {
        var product = _mapper.Map<Product>(productToAdd);
        product.UserId = userId;
        _repositoryManager.ProductsRepository.AddProduct(product);
        await _repositoryManager.SaveAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task DeleteProductByIdAsync(Guid Id)
    {
        var productToDelete = await _repositoryManager.ProductsRepository.GetProductByIdAsync(Id);
        if (productToDelete == null)
            throw new ProductNotFoundException(Id);

        _repositoryManager.ProductsRepository.DeleteProduct(productToDelete);
        await _repositoryManager.SaveAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductParameters parameters)
    {
        var products = await _repositoryManager.ProductsRepository.GetAllProductsAsync(parameters);
        var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
        return productsDto;
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid Id)
    {
        var _product = await _repositoryManager.ProductsRepository.GetProductByIdAsync(Id);
        var product = _mapper.Map<ProductDto>(_product);
        if (product is null)
            throw new ProductNotFoundException(Id);
        return product;
    }
}