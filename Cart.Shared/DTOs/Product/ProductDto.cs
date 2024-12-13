using Cart.Shared.DTOs.User;

namespace Cart.Shared.DTOs.Product;

public record class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public UserDto User { get; set; }
}