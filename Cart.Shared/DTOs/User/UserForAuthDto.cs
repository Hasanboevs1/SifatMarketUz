using System.ComponentModel.DataAnnotations;

namespace Cart.Shared.DTOs.User;

public record class UserForAuthDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}
