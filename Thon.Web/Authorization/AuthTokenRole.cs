using Thon.Web.Models;

namespace Thon.Web.Authorization;

public class AuthTokenRole : EnumString
{
    public static AuthTokenRole Admin => new AuthTokenRole("676eca1a-6350-41ab-a46f-c7b3a763c7b8");

    private AuthTokenRole(string value) : base(value) { }
}
