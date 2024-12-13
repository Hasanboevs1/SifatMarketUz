using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cart.Repository.Configurations;

public class CartEntityConfiguratin : IEntityTypeConfiguration<Entities.Models.Cart>
{
    public void Configure(EntityTypeBuilder<Entities.Models.Cart> builder)
    {
        builder.ToTable("Carts");
    }
}