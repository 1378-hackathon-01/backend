using Thon.App.Exceptions;

namespace Thon.App;

public class ThonConfiguration
{
    public ThonConfigurationInitialization Initialization { get; }

    public ThonConfigurationStorage Storage { get; }

    public ThonConfigurationHasher Hasher { get; }

    public ThonConfiguration(
        ThonConfigurationStorage storage,
        ThonConfigurationHasher hasher,
        ThonConfigurationInitialization initializaiton)
    {
        ThonArgumentException.ThrowIfNull(hasher);
        ThonArgumentException.ThrowIfNull(storage);
        ThonArgumentException.ThrowIfNull(initializaiton);

        Initialization = initializaiton;
        Storage = storage;
        Hasher = hasher;
    }
}
