using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Thon.Storage;
using Thon.App.Helpers;
using Thon.App.Services;
using Thon.App.Services.Hosted;
using Thon.App.StartupTasks;

namespace Thon.App;

public static class ThonServices
{
    public static IServiceCollection AddThonServices(this IServiceCollection services, ThonConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton(configuration.Hasher);
        services.AddSingleton(configuration.Initialization);
        services.AddSingleton<IStorageConfiguration>(configuration.Storage);

        services.AddSingleton<StorageService>();
        services.AddSingleton<Hasher>();

        services.AddSingleton<UserAdminService>();
        services.AddSingleton<UserAdminAuthService>();

        services.AddSingleton<IStartupTask, UserAdminStartupTask>();
        services.AddSingleton<IHostedService, HostedStartupTasksService>();

        return services;
    }
}
