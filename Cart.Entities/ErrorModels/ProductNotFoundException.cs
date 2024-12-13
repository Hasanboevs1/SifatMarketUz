namespace Cart.Entities.ErrorModels;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id)
        : base($"Product with {Id} was not found in our Database") { }
}
