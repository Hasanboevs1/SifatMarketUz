using Cart.Service.Contracts.Interfaces;
using Cart.Shared.DTOs.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Presentation.Controllers;

[Route("api/token")]
[Authorize]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenGenerator _tokenGenerator;
    public TokenController(ITokenGenerator tokenGenerator) => _tokenGenerator = tokenGenerator;

    [HttpPost]
    public async Task<IActionResult> GetToken([FromBody] TokenDto token)
    {
        var newToken = _tokenGenerator.RefreshToken(token);
        return Ok(new { token = newToken });
    }
}