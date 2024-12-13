using Cart.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Repository.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasData(
            new List<Role>
            {
                new Role { Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Name = "User", NormalizedName = "USER" },
            }
        );
    }
}