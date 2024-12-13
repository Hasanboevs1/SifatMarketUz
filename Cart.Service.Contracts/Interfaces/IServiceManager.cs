﻿namespace Cart.Service.Contracts.Interfaces;

public interface IServiceManager
{
    ICartService CartService { get; }
    IUserService UsersService { get; }
    IOrderService OrderService { get; }
    IProductService ProductService { get; }
    IAuthenticationService AuthenticationService { get; }
}