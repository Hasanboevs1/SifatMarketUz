using Cart.Shared.DTOs.Token;
using Cart.Shared.DTOs.User;

namespace Cart.Service.Contracts.Interfaces;

public interface IAuthenticationService
{
    Task<UserDto> RegisterUser(UserForRegisterDto user);
    Task<TokenDto> Login(UserForAuthDto user);
}
