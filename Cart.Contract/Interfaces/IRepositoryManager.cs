namespace Cart.Contract.Interfaces;

public interface IRepositoryManager
{
    IUserRepository UsersRepository { get; }
    ICartRepository CartRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductsRepository { get; }
    Task SaveAsync();
}
