namespace Thon.Core.Models;

/// <summary>
/// Пользователь учебного заведения.
/// </summary>
public class UserInstitution : BaseInstitutionModel
{
    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public string Login { get; }

    /// <summary>
    /// Полное имя пользователя.
    /// </summary>
    public string FullName { get; init; }

    /// <summary>
    /// Хеш пароля пользователя.
    /// </summary>
    public string PasswordHash { get; init; }

    /// <summary>
    /// Создание нового пользователя учебного заведения.
    /// </summary>
    /// <param name="institution">Учебное заведение.</param>
    /// <param name="login">Логин.</param>
    /// <param name="fullName">Полное имя.</param>
    /// <param name="passwordHash">Хеш пароля.</param>
    public UserInstitution(
        Institution institution,
        string login,
        string fullName,
        string passwordHash) 
        : base(institution)
    {
        Login = login;
        FullName = fullName;
        PasswordHash = passwordHash;
    }

    /// <summary>
    /// Копирование пользователя учебного заведения.
    /// </summary>
    public UserInstitution(UserInstitution model) : base(model)
    {
        Login = model.Login;
        FullName = model.FullName;
        PasswordHash = model.PasswordHash;
    }

    /// <summary>
    /// Восстановление пользователя учебного заведения.
    /// </summary>
    public UserInstitution(
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
            updatedAtUtc: updatedAtUtc)
    {
        Login = login;
        FullName = fullName;
        PasswordHash = passwordHash;
    }
}
