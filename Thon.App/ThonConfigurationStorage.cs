using Thon.App.Exceptions;
using Thon.Storage;

namespace Thon.App;

public class ThonConfigurationStorage : IStorageConfiguration
{
    public string ConnectionString { get; }

    public ThonConfigurationStorage(string connectionString)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        ConnectionString = connectionString;
    }
}
