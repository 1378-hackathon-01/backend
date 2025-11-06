using Thon.Core.Enums;
using Thon.App.Services;

namespace Thon.App.StartupTasks;

internal class UserAdminStartupTask(
    ThonConfigurationInitialization configuration,
    UserAdminService userAdminService)
    : IStartupTask
{
    public async Task Execute(CancellationToken cancellationToken = default)
    {
        var adminCount = await userAdminService.Count(cancellationToken);
        if (adminCount > 0) return;

        await userAdminService.Create(
            login: configuration.AdminLogin,
            password: configuration.AdminPassword,
            fullName: "Главный Админ",
            accessAdminUsers: AccessLevel.Write,
            accessInstitutions: AccessLevel.Write,
            accessApiTokens: AccessLevel.Write,
            cancellationToken: cancellationToken);
    }
}
