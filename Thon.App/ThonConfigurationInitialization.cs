using Thon.App.Exceptions;

namespace Thon.App;

public class ThonConfigurationInitialization
{
    public string AdminLogin { get; }

    public string AdminPassword { get; }

    public ThonConfigurationInitialization(string adminLogin, string adminPassword)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(adminLogin);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(adminPassword);

        AdminLogin = adminLogin;
        AdminPassword = adminPassword;
    }
}
