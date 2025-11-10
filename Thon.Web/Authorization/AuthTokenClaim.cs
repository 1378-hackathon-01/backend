using Thon.Web.Models;

namespace Thon.Web.Authorization;

public class AuthTokenClaim: EnumString
{
    public static AuthTokenClaim Role => new AuthTokenClaim("7fc76388-256f-4505-80b4-96b79fcd333d");

    public static AuthTokenClaim SessionId => new AuthTokenClaim("d908f039d-d082-4393-9182-caaa62197f12");

    public static AuthTokenClaim ApiTokenHash => new AuthTokenClaim("21f03e73-e803-4fe6-a0fc-372ac73ce017");

    private AuthTokenClaim(string value) : base(value) { }
}
