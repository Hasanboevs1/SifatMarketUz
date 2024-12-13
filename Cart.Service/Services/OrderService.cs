using AutoMapper;
using Cart.Contract.Interfaces;
using Cart.Entities.Models;
using Cart.Service.Contracts.Interfaces;

namespace Cart.Service.Services;

public class OrderService : IOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public OrderService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public Task<Order> CreateOrderAsync(List<CartItem> Items)
    {
        throw new NotImplementedException();
    }
}