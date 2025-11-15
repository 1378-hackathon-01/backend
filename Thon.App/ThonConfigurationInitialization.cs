using Thon.App.Exceptions;

namespace Thon.App;

public class ThonConfigurationInitialization
{
    public string AdminLogin { get; }

    public string AdminPassword { get; }

    public string ApiToken { get; }

    public ThonConfigurationInitialization(
        string adminLogin, 
        string adminPassword, 
        string apiToken)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(adminLogin);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(adminPassword);

        AdminLogin = adminLogin;
        AdminPassword = adminPassword;
        ApiToken = apiToken;
    }
}
