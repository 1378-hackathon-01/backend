using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class UserAdminStorage
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    internal UserAdminStorage(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Получить всех пользователей-администраторов.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Полный список пользователей-администраторов сервиса.</returns>
    public async Task<IReadOnlyList<UserAdmin>> Get(CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context
            .AdminUsers
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var models = entities
            .Select(x => x.GetModel())
            .ToList();

        return models;
    }

    /// <summary>
    /// Получить пользователя-администратора по его ID.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь-администратор, если он существует.</returns>
    public async Task<UserAdmin?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();
       
        var entity = await context
            .AdminUsers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        var model = entity?.GetModel();
        
        return model;
    }

    /// <summary>
    /// Получить пользователя-администратора по его логину.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь-администратор, если он найден.</returns>
    public async Task<UserAdmin?> Get(
        string login,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .AdminUsers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Login == login, cancellationToken);

        var model = entity?.GetModel();

        return model;
    }

    /// <summary>
    /// Добавить пользователя-администратора в базу данных.
    /// </summary>
    /// <param name="userAdmin">Новый пользователь.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task Insert(
        UserAdmin userAdmin,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userAdmin);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserAdminEntity(userAdmin);
        context.AdminUsers.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Обновить пользователя-администратора в базе данных.
    /// </summary>
    /// <param name="userAdmin">Изменяемый пользователь.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task Update(
        UserAdmin userAdmin,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userAdmin);

        using var context = _dbContextFactory.CreateDbContext();
        
        var entity = new UserAdminEntity(userAdmin);
        context.AdminUsers.Attach(entity);
        context.AdminUsers.Entry(entity).State = EntityState.Modified;

        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Удалить пользователя-администратора из базы данных.
    /// </summary>
    /// <param name="userAdmin">Удаляемый пользователь.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task Delete(
        UserAdmin userAdmin,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userAdmin);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserAdminEntity(userAdmin);
        context.AdminUsers.Attach(entity);
        context.AdminUsers.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Количество администраторов в базе данных.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Общее количество администраторов в базе данных.</returns>
    public async Task<int> Count(CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var count = await context
            .AdminUsers
            .CountAsync(cancellationToken);

        return count;
    }
}
