
using System.Diagnostics.CodeAnalysis;

namespace Thon.App.Exceptions;

public class ThonArgumentException : ThonException
{
    public ThonArgumentException() { }

    public ThonArgumentException(string? message) : base(message) { }

    public ThonArgumentException(string? message, Exception? innerException) : base(message, innerException) { }

    public static void ThrowIf(bool expression, string? message = null)
    {
        if (expression)
            throw new ThonArgumentException(message);
    }

    public static void ThrowIfNull([NotNull] object? value, string? message = null)
    {
        ThrowIf(value is null, message);
    }

    public static void ThrowIfNullOrEmpty([NotNull] string? value, string? message = null)
    {
        if (string.IsNullOrEmpty(value))
            throw new ThonArgumentException(message);
    }

    public static void ThrowIfNullOrWhiteSpace([NotNull] string? value, string? message = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ThonArgumentException(message);
    }
}
