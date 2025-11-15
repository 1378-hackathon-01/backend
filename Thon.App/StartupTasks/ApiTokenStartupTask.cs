using Thon.App.Services;

namespace Thon.App.StartupTasks;

internal class ApiTokenStartupTask(
    ThonConfigurationInitialization configuration,
    ApiTokenService tokenService)
    : IStartupTask
{
    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var tokens = await tokenService.Get(cancellationToken);
        if (tokens.Count > 0) return;

        await tokenService.Create(
            token: configuration.ApiToken, 
            cancellationToken: cancellationToken);
    }
}