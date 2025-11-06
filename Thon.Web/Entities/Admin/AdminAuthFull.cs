using Thon.Core.Models;

namespace Thon.Web.Entities.Admin;

public class AdminAuthFull(UserAdminAuth auth)
{
    public Guid Id { get; } = auth.Id;

    public string? UserAgent { get; } = auth.DeviceUserAgent;

    public string? IpAddress { get; } = auth.DeviceIpAddress;

    public DateTime CreatedAtUtc { get; } = auth.CreatedAtUtc;
}
