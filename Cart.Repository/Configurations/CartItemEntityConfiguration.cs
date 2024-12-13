using Cart.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cart.Repository.Configurations;

public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");
        builder.HasOne(x => x.Cart).WithMany(p => p.CartItems).HasForeignKey(p => p.CartId);
        builder.HasOne(x => x.Product).WithMany(p => p.CartItems).HasForeignKey(p => p.ProductId);
    }
}
