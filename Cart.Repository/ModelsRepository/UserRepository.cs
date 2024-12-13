using Cart.Contract.Interfaces;
using Cart.Entities.Models;
using Cart.Repository.Base;
using Cart.Repository.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cart.Repository.ModelsRepository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext context)
        : base(context) { }

    public async Task<IEnumerable<User>> GetUsersAsync() => await FindAll().ToArrayAsync();

    public async Task<User> GetUserByIdAsync(Guid Id) =>
        await FindByCondition(user => user.Id.ToString() == Id.ToString()).FirstOrDefaultAsync();

    public async void DeleteUser(User user) => Delete(user);
}