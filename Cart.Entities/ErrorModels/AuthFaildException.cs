namespace Cart.Entities.ErrorModels;


public class AuthFaildException : Exception
{
    public AuthFaildException(string message)
        : base(message) { }
}