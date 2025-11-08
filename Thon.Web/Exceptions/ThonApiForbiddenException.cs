
using Thon.Core.Enums;

namespace Thon.Web.Exceptions;

public class ThonApiForbiddenException : ThonApiException
{
    public ThonApiForbiddenException() { }

    public ThonApiForbiddenException(string? message) : base(message) { }

    public ThonApiForbiddenException(string? message, Exception? innerException) : base(message, innerException) { }

    public static void ThrowIfLess(
        AccessLevel value,
        AccessLevel target,
        string? message = null)
    {
        if (value < target)
            throw new ThonApiForbiddenException(message);
    }
}
