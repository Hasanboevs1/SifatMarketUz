namespace Cart.Entities.ErrorModels;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
        : base(message) { }
}