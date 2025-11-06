using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class UserAdminAuthEntity : BaseModelEntity, IEntity<UserAdminAuth>
{
    public Guid UserId { get; set; }

    public string? DeviceUserAgent { get; set; }

    public string? DeviceIpAddress { get; set; }

    public UserAdminAuthEntity(UserAdminAuth model) : base(model)
    {
        UserId = model.UserId;
        DeviceUserAgent = model.DeviceUserAgent;
        DeviceIpAddress = model.DeviceIpAddress;
    }

    public UserAdminAuthEntity(
        Guid id,
        Guid userId,
        string? deviceUserAgent,
        string? deviceIpAddress,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc:updatedAtUtc)
    {
        UserId = userId;
        DeviceUserAgent = deviceUserAgent;
        DeviceIpAddress = deviceIpAddress;
    }

    public new UserAdminAuth GetModel()
    {
        return new UserAdminAuth(
            id: Id,
            userId: UserId,
            deviceUserAgent: DeviceUserAgent,
            deviceIpAddress: DeviceIpAddress,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
