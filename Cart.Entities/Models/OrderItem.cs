using Cart.Entities.Enums;

namespace Cart.Entities.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
