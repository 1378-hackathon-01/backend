using Thon.Core.Enums;
using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class UserAdminEntity : BaseModelEntity, IEntity<UserAdmin>
{
    public string Login { get; set; }

    public string PasswordHash { get; set; }

    public string FullName { get; set; }

    public AccessLevel AccessAdminUsers { get; set; }

    public AccessLevel AccessInstitutions { get; set; }

    public AccessLevel AccessApiTokens { get; set; }

    public UserAdminEntity(
        Guid id,
        string login,
        string passwordHash,
        string fullName,
        AccessLevel accessAdminUsers,
        AccessLevel accessInstitutions,
        AccessLevel accessApiTokens,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        Login = login;
        PasswordHash = passwordHash;
        FullName = fullName;
        AccessAdminUsers = accessAdminUsers;
        AccessInstitutions = accessInstitutions;
        AccessApiTokens = accessApiTokens;
    }

    public UserAdminEntity(UserAdmin model) : base(model)
    {
        Login = model.Login;
        PasswordHash = model.PasswordHash;
        FullName = model.FullName;
        AccessAdminUsers = model.AccessAdminUsers;
        AccessInstitutions = model.AccessInstitutions;
        AccessApiTokens = model.AccessApiTokens;
    }

    public new UserAdmin GetModel()
    {
        return new UserAdmin(
            id: Id,
            login: Login,
            passwordHash: PasswordHash,
            fullName: FullName,
            accessAdminUsers: AccessAdminUsers,
            accessInstitutions: AccessInstitutions,
            accessApiTokens: AccessApiTokens,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
