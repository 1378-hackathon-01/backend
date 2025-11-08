using Thon.Core.Models;

namespace Thon.Web.Entities.Admin;

public class AdminApiTokenFull(ApiToken token)
{
    public Guid Id { get; } = token.Id;

    public DateTime CreatedAtUtc { get; } = token.CreatedAtUtc;
}
