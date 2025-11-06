using Thon.App.Exceptions;

namespace Thon.Web.Exceptions;

public class ThonApiException : ThonException
{
    public ThonApiException() { }

    public ThonApiException(string? message) : base(message) { }

    public ThonApiException(string? message, Exception? innerException) : base(message, innerException) { }
}
