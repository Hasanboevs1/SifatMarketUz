using System.Security.Claims;

namespace Cart.Presentation.Helpers;

public static class UserHelpers
{
    public static string GetUserId(ClaimsPrincipal User) => User.FindFirst("Id").Value;
}
