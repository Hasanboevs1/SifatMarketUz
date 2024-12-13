namespace Cart.Contract.Interfaces;

public interface ICartRepository : IRepositoryBase<Entities.Models.Cart>
{
    Task<Entities.Models.Cart?> GetCartByIdAsync(Guid Id);
    Task<Entities.Models.Cart?> GetCartByUserIdAsync(string UserId);
    Task<IEnumerable<Entities.Models.Cart>> GetAllCarts(string UserId);
}
