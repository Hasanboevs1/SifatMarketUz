using Cart.Entities.Models;

namespace Cart.Contract.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid Id);
    void DeleteUser(User user);
}