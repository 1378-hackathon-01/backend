namespace Thon.App.Exceptions;

public class ThonConflictException : ThonException
{
    public ThonConflictException()
    {
    }

    public ThonConflictException(string? message) : base(message)
    {
    }

    public ThonConflictException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
