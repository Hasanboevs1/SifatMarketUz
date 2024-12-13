namespace Cart.Entities.Models;

public class CartItem
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Models.Cart Cart { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}
