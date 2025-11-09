using Thon.App.Exceptions;
using Thon.App.Helpers;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class UserInstitutionService(
    StorageService storage,
    Hasher hasher)
{
    public async Task<UserInstitution?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user = await storage.UserInstitutions.Get(
            id: id, 
            cancellationToken: cancellationToken);

        return user;
    }

    public async Task<UserInstitution> Create(
        Institution institution,
        string login,
        string password,
        string fullName,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(institution);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(login);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(password);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(fullName);

        login = login.Trim().ToLower();
        password = password.Trim();
        fullName = fullName.Trim();

        var user = new UserInstitution(
            institution: institution,
            login: login,
            fullName: fullName,
            passwordHash: hasher.Sha256Salted(password));

        await storage.UserInstitutions.Insert(
            user: user,
            cancellationToken: cancellationToken);

        return user;
    }
}
