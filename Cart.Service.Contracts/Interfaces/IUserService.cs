using Cart.Shared.DTOs.User;

namespace Cart.Service.Contracts.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserByIdAsync(Guid Id);

    Task DeleteUserByIdAsync(Guid Id);
}