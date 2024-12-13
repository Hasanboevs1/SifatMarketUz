using Cart.Entities.Models;

namespace Cart.Service.Contracts.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(List<CartItem> Items);
}