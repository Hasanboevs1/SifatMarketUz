﻿using Cart.Entities.ErrorModels;
using Cart.Entities.JwtModel;
using Cart.Entities.Models;
using Cart.Service.Contracts.Interfaces;
using Cart.Shared.DTOs.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cart.Service.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly UserManager<User> _userManager;
    private readonly JWTOptions _jwtOptions;
    private User? _user;

    public TokenGenerator(
        UserManager<User> userManager,
        IOptions<JWTOptions> options
    )
    {
        _userManager = userManager;
        _jwtOptions = options.Value;

    }

    public async Task<TokenDto> CreateToken(User _user)
    {
        // create signing credentials
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));
        var secret = new SymmetricSecurityKey(key);
        var signingCred = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);


        var claims = new List<Claim>
        {
            new Claim("Id", _user.Id),
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim(ClaimTypes.Email, _user.Email)
        };

        // set roles in claims 
        var roles = await _userManager.GetRolesAsync(_user);
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
        claims.AddRange(roleClaims);

        var refreshToken = GenerateRefreshToken();
        _user.RefreshToken = refreshToken;
        _user.ExpireTime = DateTime.Now.AddDays(1);

        await _userManager.UpdateAsync(_user);

        var jwtToken = GetJwtSecurityToken(signingCred, claims);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        var token = new TokenDto() { AccessToken = accessToken, RefreshToken = refreshToken };
        return token;
    }

    public async Task<TokenDto> RefreshToken(TokenDto token)
    {
        var principle = GetClaimsPrincipalFromExpiredToken(token.AccessToken);
        var email = principle.Claims.Where(claim => claim.Type == ClaimTypes.Email).First().Value;
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || user.RefreshToken != token.RefreshToken || user.ExpireTime <= DateTime.Now)
            throw new RefershTokenBadRequestException();

        var newToken = await CreateToken(user);
        return newToken;
    }

    private string GenerateRefreshToken()
    {
        var randNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randNumber);
            return Convert.ToBase64String(randNumber);
        }
    }

    private ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
    {
        var tokenVlaidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"))),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = _jwtOptions.validIssuer,
            ValidAudience = _jwtOptions.validAudience
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principle = tokenHandler.ValidateToken(token, tokenVlaidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principle;
    }

    private JwtSecurityToken GetJwtSecurityToken(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.validIssuer,
            audience: _jwtOptions.validAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtOptions.expires)),
            signingCredentials: signingCredentials
        );

        return jwtToken;
    }
}