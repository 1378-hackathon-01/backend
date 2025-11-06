
namespace Thon.Web.Exceptions;

public class ThonApiUnauthorizedException : ThonApiException
{
    public ThonApiUnauthorizedException() { }

    public ThonApiUnauthorizedException(string? message) : base(message) { }

    public ThonApiUnauthorizedException(string? message, Exception? innerException) : base(message, innerException) { }
}
