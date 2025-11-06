using Thon.App.Exceptions;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class UserAdminAuthService(StorageService storage)
{
    public async Task<UserAdminAuth> CreateAuth(
        UserAdmin userAdmin,
        string? deviceUserAgent = null,
        string? deviceIpAddress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userAdmin);

        var auth = new UserAdminAuth(userId: userAdmin.Id)
        {
            DeviceIpAddress = deviceIpAddress,
            DeviceUserAgent = deviceUserAgent,
        };

        await storage
            .UserAdminAuths
            .Insert(
                auth: auth, 
                cancellationToken: cancellationToken);

        return auth;
    }

    public async Task<IReadOnlyList<UserAdminAuth>> Get(
        UserAdmin admin, 
        CancellationToken cancellationToken = default)
    {
        var auth = await storage
            .UserAdminAuths
            .Get(
                user: admin,
                cancellationToken: cancellationToken);

        return auth;
    }

    public async Task<UserAdminAuth?> Get(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var auth = await storage
            .UserAdminAuths
            .Get(
                id: id,
                cancellationToken: cancellationToken);

        return auth;
    }

    public async Task Delete(
        UserAdminAuth auth, 
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(auth);

        await storage
            .UserAdminAuths
            .Delete(
                auth: auth,
                cancellationToken: cancellationToken);
    }
}
