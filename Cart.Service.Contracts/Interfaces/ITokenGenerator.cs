using Cart.Entities.Models;
using Cart.Shared.DTOs.Token;

namespace Cart.Service.Contracts.Interfaces;

public interface ITokenGenerator
{
    public Task<TokenDto> CreateToken(User user);
    public Task<TokenDto> RefreshToken(TokenDto token);
}
