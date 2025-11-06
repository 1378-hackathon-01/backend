using Thon.Storage.Storages;

namespace Thon.Storage;

public class StorageService
{
    public UserAdminStorage UserAdmins { get; }

    public UserAdminAuthStorage UserAdminAuths { get; }

    public StorageService(IStorageConfiguration configuration)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(configuration.ConnectionString);

        var dbContextFactory = new DatabaseContextFactory(configuration.ConnectionString);
        UserAdminAuths = new UserAdminAuthStorage(dbContextFactory);
        UserAdmins = new UserAdminStorage(dbContextFactory);
    }
}
