
namespace Thon.Web.Exceptions;

public class ThonApiConflictException : ThonApiException
{
    public ThonApiConflictException() { }

    public ThonApiConflictException(string? message) : base(message) { }

    public ThonApiConflictException(string? message, Exception? innerException) : base(message, innerException) { }
}
