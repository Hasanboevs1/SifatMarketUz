using Cart.Contract.Interfaces;
using Cart.Repository.Base;
using Cart.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cart.Repository.ModelsRepository;

public class CartRepository : RepositoryBase<Entities.Models.Cart>, ICartRepository
{
    private readonly RepositoryContext _context;
    public CartRepository(RepositoryContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Entities.Models.Cart?> GetCartByIdAsync(Guid Id) =>
                await FindByCondition(cart => cart.Id == Id)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();
    public async Task<Entities.Models.Cart?> GetCartByUserIdAsync(string UserId) =>
                await FindByCondition(cart => cart.UserId == UserId)
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();

    public async Task<IEnumerable<Entities.Models.Cart>> GetAllCarts(string UserId) => await FindByCondition(cart => cart.UserId == UserId).ToListAsync();
}