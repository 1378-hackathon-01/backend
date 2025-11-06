using Thon.Core.Enums;

namespace Thon.Core.Models;

public class UserAdmin : BaseModel
{
    /// <summary>
    /// Логин администратора.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Хеш пароля администратора.
    /// </summary>
    public string PasswordHash { get; init; }

    /// <summary>
    /// Полное имя администратора.
    /// </summary>
    public string FullName { get; init; }

    /// <summary>
    /// Уровень доступа администратора к управлению другими администраторами.
    /// </summary>
    public AccessLevel AccessAdminUsers { get; init; }

    /// <summary>
    /// Уровень доступа администратора к управлению списком учебных заведений.
    /// </summary>
    public AccessLevel AccessInstitutions { get; init; }

    /// <summary>
    /// Уровень доступа к упрвлению API-токенами приложения.
    /// </summary>
    public AccessLevel AccessApiTokens { get; init; }
    
    /// <summary>
    /// Создать нового пользователя.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="passwordHash">Хеш пароля пользователя.</param>
    /// <param name="fullName">Полное имя пользователя.</param>
    public UserAdmin(
        string login,
        string passwordHash,
        string fullName)
    {
        Login = login;
        PasswordHash = passwordHash;
        FullName = fullName;
    }

    /// <summary>
    /// Копирование пользователя.
    /// </summary>
    /// <param name="model">Копируемый пользователь.</param>
    public UserAdmin(UserAdmin model) : base(model)
    {
        Login = model.Login;
        PasswordHash = model.PasswordHash;
        FullName = model.FullName;
        AccessAdminUsers = model.AccessAdminUsers;
        AccessInstitutions = model.AccessInstitutions;
        AccessApiTokens = model.AccessApiTokens;
    }

    /// <summary>
    /// Восстановление пользователя.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="passwordHash">Хеш пароля пользователя.</param>
    /// <param name="fullName">Полное имя пользователя.</param>
    /// <param name="accessAdminUsers">Доступ к изменению других администраторов.</param>
    /// <param name="accessInstitutions">Доступ к изменению учебных заведений.</param>
    /// <param name="createdAtUtc">Дата создания пользователя по UTC.</param>
    /// <param name="updatedAtUtc">Дата изменения пользователя по UTC.</param>
    public UserAdmin(
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
}
