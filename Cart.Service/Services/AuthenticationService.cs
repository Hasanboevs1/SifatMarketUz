using AutoMapper;
using Cart.Contract.Interfaces;
using Cart.Entities.ErrorModels;
using Cart.Entities.Models;
using Cart.Service.Contracts.Interfaces;
using Cart.Shared.DTOs.Token;
using Cart.Shared.DTOs.User;
using Microsoft.AspNetCore.Identity;

namespace Cart.Service.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private User? _user;
    // private readonly RepositoryContext _context;

    public AuthenticationService(
        IRepositoryManager repositoryManager,
        IMapper mapper,
        UserManager<User> userManager,
        ITokenGenerator tokenGenerator
    // RepositoryContext context
    )
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        // _context = context;
    }

    public async Task<UserDto> RegisterUser(UserForRegisterDto user)
    {
        _user = _mapper.Map<User>(user);
        var result = await _userManager.CreateAsync(_user, user.Password);
        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Error while creating user");

        var _userDto = _mapper.Map<UserDto>(_user);
        return _userDto;
    }

    public async Task<TokenDto> Login(UserForAuthDto user)
    {
        _user = await _userManager.FindByEmailAsync(user.Email);
        var succeeded = _user != null && await _userManager.CheckPasswordAsync(_user, user.Password);
        if (!succeeded)
            throw new UserLoginFaildException();
        return await _tokenGenerator.CreateToken(_user);
    }
}