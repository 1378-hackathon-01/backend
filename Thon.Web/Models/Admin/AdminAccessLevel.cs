using Thon.Core.Enums;

namespace Thon.Web.Models.Admin;

public class AdminAccessLevel : EnumString
{
    public static AdminAccessLevel Undefined = new AdminAccessLevel("undefined");

    public static AdminAccessLevel None = new AdminAccessLevel("none");

    public static AdminAccessLevel Read = new AdminAccessLevel("read");

    public static AdminAccessLevel Write = new AdminAccessLevel("write");

    private AdminAccessLevel(string value): base(value) { }

    public static AdminAccessLevel Parse(AccessLevel access)
    {
        if (access == AccessLevel.None) return None;
        if (access == AccessLevel.Read) return Read;
        if (access == AccessLevel.Write) return Write;
        return Undefined;
    }
}
