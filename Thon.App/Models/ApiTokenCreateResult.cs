using Thon.Core.Models;

namespace Thon.App.Models;

public class ApiTokenCreateResult(string tokenRaw, ApiToken token)
{
    public string TokenRaw { get; } = tokenRaw;

    public ApiToken Token { get; } = token;
}
