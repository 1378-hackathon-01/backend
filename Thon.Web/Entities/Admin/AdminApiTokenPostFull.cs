using Thon.App.Models;

namespace Thon.Web.Entities.Admin;

public class AdminApiTokenPostFull(ApiTokenCreateResult createResult)
{
    public string Token { get; } = createResult.Token;
}
