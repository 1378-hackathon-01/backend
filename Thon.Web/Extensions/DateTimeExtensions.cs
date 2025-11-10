namespace Thon.Web.Extensions;

public static class DateTimeExtensions
{
    public static long ToUnixTimeMilliseconds(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
}
