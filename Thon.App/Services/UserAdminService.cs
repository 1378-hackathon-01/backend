using Thon.Core.Models;
using Thon.Core.Enums;
using Thon.Storage;
using Thon.App.Helpers;
using Thon.App.Exceptions;

namespace Thon.App.Services;

/// <summary>
/// Сервис управления администраторами.
/// </summary>
/// <param name="storage">Сервис-хранилище.</param>
/// <param name="hasher">Генератор хешей.</param>
public class UserAdminService(StorageService storage, Hasher hasher)
{
    /// <summary>
    /// Получить администратора по его логину и паролю.
    /// </summary>
    /// <param name="login">Логин администратора.</param>
    /// <param name="password">Пароль администратора.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Администратор, если он был найден.</returns>
    public async Task<UserAdmin?> Get(
        string login,
        string password,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(login);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(password);

        login = login.Trim().ToLower();
        password = password.Trim();

        var admin = await storage
            .UserAdmins
            .Get(
                login: login, 
                cancellationToken: cancellationToken);

        if (admin is null) 
            return null;

        if (!hasher.VerifySha256Salted(password, admin.PasswordHash)) 
            return null;

        return admin;
    }

    public async Task<UserAdmin?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var admin = await storage
            .UserAdmins
            .Get(
                id: id,
                cancellationToken: cancellationToken);

        return admin;
    }

    /// <summary>
    /// Создать нового администратора.
    /// </summary>
    /// <param name="login">Логин администратора.</param>
    /// <param name="password">Пароль администратора.</param>
    /// <param name="fullName">Полное имя администратора.</param>
    /// <param name="accessAdminUsers">Уровень доступа к другим администраторам.</param>
    /// <param name="accessInstitutions">Уровень доступа к учебным заведениям.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданный администратор.</returns>
    /// <exception cref="ThonConflictException">Логин уже используется.</exception>
    public async Task<UserAdmin> Create(
        string login,
        string password,
        string fullName,
        AccessLevel accessAdminUsers,
        AccessLevel accessInstitutions,
        AccessLevel accessApiTokens,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(login);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(password);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(fullName);

        login = login.Trim().ToLower();
        password = password.Trim();
        fullName = fullName.Trim();

        var conflictAdmin = await storage
            .UserAdmins
            .Get(
                login: login, 
                cancellationToken: cancellationToken);

        if (conflictAdmin is not null) 
            throw new ThonConflictException("User with that login already exists!");

        var admin = new UserAdmin(
            login: login,
            passwordHash: hasher.Sha256Salted(password),
            fullName: fullName)
        { 
            AccessAdminUsers = accessAdminUsers, 
            AccessInstitutions = accessInstitutions,
            AccessApiTokens = accessApiTokens,
        };

        await storage
            .UserAdmins
            .Insert(
                userAdmin: admin, 
                cancellationToken: cancellationToken);

        return admin;
    }

    public async Task<UserAdmin> Update(
        UserAdmin admin,
        string? newPassword = null,
        string? newFullName = null,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(admin);

        if (newPassword is not null)
        {
            newPassword = newPassword.Trim();

            if (newPassword.Length < 4)
                throw new ThonArgumentException("Invalid Password Length!");

            admin = new UserAdmin(admin) 
            { 
                PasswordHash = hasher.Sha256Salted(newPassword) 
            };
        }

        if (newFullName is not null)
        {
            newFullName = newFullName.Trim();

            if (newFullName.Length < 4)
                throw new ThonArgumentException("Invalid Full Name Length!");

            admin = new UserAdmin(admin)
            {
                FullName = newFullName
            };
        }

        await storage
            .UserAdmins
            .Update(
                userAdmin: admin, 
                cancellationToken: cancellationToken);

        return admin;
    }

    public async Task<int> Count(CancellationToken cancellationToken = default)
    {
        var count = await storage
            .UserAdmins
            .Count(cancellationToken);

        return count;
    }
}
