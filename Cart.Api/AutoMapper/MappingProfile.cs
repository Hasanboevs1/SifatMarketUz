using AutoMapper;
using Cart.Entities.Models;
using Cart.Shared.DTOs.Cart;
using Cart.Shared.DTOs.Product;
using Cart.Shared.DTOs.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cart.Api.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserForRegisterDto>().ReverseMap();


        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductToAddDto>().ReverseMap();


        CreateMap<Entities.Models.Cart, CartDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap();
    }
}
