using AspNetCoreRateLimit;
using Cart.Api.AutoMapper;
using Cart.Contract.Interfaces;
using Cart.Entities.JwtModel;
using Cart.Entities.Models;
using Cart.Presentation.Controllers;
using Cart.Repository.Contexts;
using Cart.Repository.Manager;
using Cart.Service.Contracts.Interfaces;
using Cart.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cart.Api.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services
            .AddControllers(config => config.RespectBrowserAcceptHeader = true)
            .AddApplicationPart(typeof(UserController).Assembly);
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(
            opts =>
                opts.AddDefaultPolicy(
                    policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
                )
        );
    }

    public static void ConfigureApiBehaviorOptions(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(opts =>
        {
            // To enable our custom responses from the actions for validation
            opts.SuppressModelStateInvalidFilter = true;
        });
    }

    public static void ConfigureSqlServer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<RepositoryContext>(o => o.UseSqlite("Data Source=main.db"));

        using var sp = services.BuildServiceProvider();
        var context = sp.GetService<RepositoryContext>();
        context?.Database.Migrate();
        context?.Database.EnsureCreated();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection service) =>
        service.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(typeof(MapperProfile).Assembly);

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>()
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(config =>
        {
            config.Password.RequireLowercase = true;
            config.Password.RequireDigit = true;
            config.User.AllowedUserNameCharacters = String.Empty;
            config.User.RequireUniqueEmail = true;
        });
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
        services.AddTransient<ITokenGenerator, TokenGenerator>();

        var jwtSettings = configuration.GetSection("JWTOptions");
        var secretKey = jwtSettings["SecretKey"] ?? throw new ArgumentNullException("SecretKey");

        services
            .AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
    }

    public static void ConfigureRateLimitMiddleware(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMemoryCache();
        services.AddHttpContextAccessor();
        services.AddInMemoryRateLimiting();

        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
        services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
    }
}
