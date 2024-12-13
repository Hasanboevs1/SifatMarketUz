using System.ComponentModel.DataAnnotations.Schema;

namespace Cart.Entities.Models;

public class Cart
{
    public Guid Id { get; set; }
    public string UserId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}
