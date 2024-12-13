using Cart.Entities.Enums;

namespace Cart.Entities.Models;

public class Order
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public string UserId { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime UpdatedAt { get; set; }
}
