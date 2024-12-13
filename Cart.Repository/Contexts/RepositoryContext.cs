using Cart.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cart.Repository.Contexts;


public class RepositoryContext : IdentityDbContext<User>
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Entities.Models.Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(
            typeof(Configurations.UserEntityConfiguration).Assembly
        );
    }
}