using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Thon.Web.Authorization;

public class AuthOptions
{
    public string Issuer { get; } = "dcd399da-76b1-4cb1-91c4-4c573c831c6f";

    public string Audience { get; } = "b34be40d-2318-4272-ba06-9202dad6f3b4";

    public string Key { get; } = "aGrxAEnfdywcGLJaeKriBBDoCCNBhnTlmbsdEoULyvexoGjyossVQSwTAsfddDnfZicNeQyewRedzQMTJkAVlxfzlsIlHwtBEDyk";

    public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
