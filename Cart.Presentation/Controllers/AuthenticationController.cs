using Cart.Presentation.Filters;
using Cart.Service.Contracts.Interfaces;
using Cart.Shared.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Presentation.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AuthenticationController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;

    [HttpPost("register", Name = "registerNewUser")]
    [ServiceFilter(typeof(ValidationActionAttribute))]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto user)
    {
        var _user = await _serviceManager.AuthenticationService.RegisterUser(user);
        return Ok(new { _user });
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationActionAttribute))]
    public async Task<IActionResult> Login([FromBody] UserForAuthDto user)
    {
        var tokens = await _serviceManager.AuthenticationService.Login(user);
        return Ok(new { tokens });
    }
}
