using AutoMapper;
using Cart.Contract.Interfaces;
using Cart.Entities.JwtModel;
using Cart.Entities.Models;
using Cart.Service.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Cart.Service.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> userService;
    private readonly Lazy<IProductService> productService;
    private readonly Lazy<IAuthenticationService> authService;
    private readonly Lazy<ICartService> cartService;
    private readonly Lazy<IOrderService> orderService;

    public ServiceManager(
        IRepositoryManager repoManager,
    IMapper mapper,
    UserManager<User> userManager,
        IOptions<JWTOptions> options,
        ITokenGenerator tokenGenerator
    )
    {
        userService = new(() => new UserService(repoManager, mapper));
        cartService = new(() => new CartService(repoManager, mapper));
        productService = new(() => new ProductService(repoManager, mapper));
        orderService = new(() => new OrderService(repoManager, mapper));
        authService = new(
            () => new AuthenticationService(repoManager, mapper, userManager, tokenGenerator)
        );
    }

    public ICartService CartService => cartService.Value;
    public IUserService UsersService => userService.Value;
    public IOrderService OrderService => orderService.Value;
    public IProductService ProductService => productService.Value;
    public IAuthenticationService AuthenticationService => authService.Value;

}
