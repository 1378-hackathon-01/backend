using Thon.Core.Models;
using Thon.Web.Models.Admin;

namespace Thon.Web.Entities.Admin;

public class AdminUserFull
{
    public Guid Id { get; }

    public string Login { get; }

    public string FullName { get; }

    public AdminAccessLevel AccessAdmins { get; }

    public AdminAccessLevel AccessInstitutions { get; }

    public AdminAccessLevel AccessTokens { get; }

    public AdminUserFull(UserAdmin admin)
    {
        Id = admin.Id;
        Login = admin.Login;
        FullName = admin.FullName;
        AccessTokens = AdminAccessLevel.Parse(admin.AccessApiTokens);
        AccessAdmins = AdminAccessLevel.Parse(admin.AccessAdminUsers);
        AccessInstitutions = AdminAccessLevel.Parse(admin.AccessInstitutions);
    }
}
