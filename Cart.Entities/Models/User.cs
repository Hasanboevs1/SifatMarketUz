using Microsoft.AspNetCore.Identity;

namespace Cart.Entities.Models;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime ExpireTime { get; set; }
}
