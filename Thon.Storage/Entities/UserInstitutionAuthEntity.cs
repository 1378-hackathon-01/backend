using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class UserInstitutionAuthEntity : BaseModelEntity, IEntity<UserInstitutionAuth>
{
    public Guid UserId { get; set; }

    public string? DeviceUserAgent { get; set; }

    public string? DeviceIpAddress { get; set; }

    public UserInstitutionAuthEntity(UserInstitutionAuth model) : base(model)
    {
        UserId = model.UserId;
        DeviceUserAgent = model.DeviceUserAgent;
        DeviceIpAddress = model.DeviceIpAddress;
    }

    public UserInstitutionAuthEntity(
        Guid id,
        Guid userId,
        string? deviceUserAgent,
        string? deviceIpAddress,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        UserId = userId;
        DeviceUserAgent = deviceUserAgent;
        DeviceIpAddress = deviceIpAddress;
    }

    public new UserInstitutionAuth GetModel()
    {
        return new UserInstitutionAuth(
            id: Id,
            userId: UserId,
            deviceUserAgent: DeviceUserAgent,
            deviceIpAddress: DeviceIpAddress,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}