
using System.Diagnostics.CodeAnalysis;

namespace Thon.Web.Exceptions;

public class ThonApiNotFoundException : ThonApiException
{
    public ThonApiNotFoundException() { }

    public ThonApiNotFoundException(string? message) : base(message) { }

    public ThonApiNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    public static void ThrowIfNull([NotNull] object? value, string? message = null)
    {
        if (value is null) 
            throw new ThonApiNotFoundException(message);
    }
}
