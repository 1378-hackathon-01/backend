namespace Thon.App.Exceptions;

public class ThonException : Exception
{
    public ThonException()
    {
    }

    public ThonException(string? message) : base(message)
    {
    }

    public ThonException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
