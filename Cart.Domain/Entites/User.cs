using Microsoft.AspNetCore.Identity;

namespace Cart.Domain.Entites;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Product> Products { get; set; }
    public ICollection<Order> Orders { get; set; }

}
