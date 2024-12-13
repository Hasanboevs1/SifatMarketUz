namespace Cart.Entities.ErrorModels;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string Id)
        : base($"User with {Id} was not found.") { }
}
