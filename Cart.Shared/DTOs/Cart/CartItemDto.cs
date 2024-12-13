using Cart.Shared.DTOs.Product;

namespace Cart.Shared.DTOs.Cart;

public class CartItemDto
{
    public ProductDto Product { get; set; }
    public int Quantity { get; set; }
}