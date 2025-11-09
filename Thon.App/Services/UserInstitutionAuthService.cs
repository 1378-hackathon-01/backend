using Thon.App.Exceptions;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class UserInstitutionAuthService(StorageService storage)
{
    public async Task<UserInstitutionAuth> Create(
        UserInstitution user, 
        string? deviceUserAgent = null,
        string? deviceIpAddress = null,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(user);

        var auth = new UserInstitutionAuth(user: user)
        {
            DeviceIpAddress = deviceIpAddress,
            DeviceUserAgent = deviceUserAgent,
        };

        await storage.UserInstitutionAuths
            .Insert(
                auth: auth,
                cancellationToken: cancellationToken);

        return auth;
    }

    public async Task<UserInstitutionAuth?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var auth = await storage
            .UserInstitutionAuths
            .Get(
                id: id,
                cancellationToken: cancellationToken);

        return auth;
    }
}
