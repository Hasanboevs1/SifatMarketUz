namespace Cart.Domain.Entites;

public class Order
{
    public int Id { get; set; }
    public ICollection<Product> Products { get; set; }
    public decimal TotalCost => Products.Sum(x => x.Price);
}