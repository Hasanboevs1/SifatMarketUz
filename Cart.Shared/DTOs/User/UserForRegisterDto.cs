namespace Cart.Shared.DTOs.User;

public record class UserForRegisterDto
{
    public string Email { get; set; }

    public string Password { get; set; }
    public string Username { get; set; }
};