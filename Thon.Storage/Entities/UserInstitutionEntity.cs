using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class UserInstitutionEntity : BaseInstitutionModelEntity, IEntity<UserInstitution>
{
    public string Login { get; set; }

    public string FullName { get; set; }

    public string PasswordHash { get; set; }

    public UserInstitutionEntity(UserInstitution model) : base(model)
    {
        Login = model.Login;
        FullName = model.FullName;
        PasswordHash = model.PasswordHash;
    }

    public UserInstitutionEntity(
        Guid id,
        Guid institutionId,
        string login,
        string fullName,
        string passwordHash,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            institutionId: institutionId,
            createdAtUtc: createdAtUtc,
            updatedAtUtc:updatedAtUtc)
    {
        Login = login;
        FullName = fullName;
        PasswordHash = passwordHash;
    }

    public new UserInstitution GetModel()
    {
        return new UserInstitution(
            id: Id,
            institutionId: InstitutionId,
            login: Login,
            fullName: FullName,
            passwordHash: PasswordHash,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
