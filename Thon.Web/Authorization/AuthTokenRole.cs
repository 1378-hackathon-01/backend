using Thon.Web.Models;

namespace Thon.Web.Authorization;

public class AuthTokenRole : EnumString
{
    public static AuthTokenRole Admin => new AuthTokenRole("676eca1a-6350-41ab-a46f-c7b3a763c7b8");

    public static AuthTokenRole Institution => new AuthTokenRole("1cb36ab3-6554-46d6-8e6c-5bb0a2886c6a");

    private AuthTokenRole(string value) : base(value) { }
}
