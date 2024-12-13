namespace Cart.Entities.ErrorModels;

public class RefershTokenBadRequestException : Exception
{
    public RefershTokenBadRequestException() : base("Error in validating RefershToken")
    {
    }
}
