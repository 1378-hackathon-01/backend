namespace Thon.App.Exceptions;

public class ThonNotFoundException : ThonException
{
    public ThonNotFoundException()
    {
    }

    public ThonNotFoundException(string? message) : base(message)
    {
    }

    public ThonNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
