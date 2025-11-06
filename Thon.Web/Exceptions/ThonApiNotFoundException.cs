
namespace Thon.Web.Exceptions;

public class ThonApiNotFoundException : ThonApiException
{
    public ThonApiNotFoundException() { }

    public ThonApiNotFoundException(string? message) : base(message) { }

    public ThonApiNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
}
