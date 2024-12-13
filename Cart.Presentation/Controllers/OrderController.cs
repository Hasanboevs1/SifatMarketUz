using Cart.Entities.Models;
using Cart.Presentation.Helpers;
using Cart.Service.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Presentation.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public OrderController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> CreateOrder([FromBody] List<CartItem> cartItems)
    {
        var UserId = UserHelpers.GetUserId(User);
        var order = await _serviceManager.OrderService.CreateOrderAsync(cartItems);
        return Ok(new { order });
    }
}
