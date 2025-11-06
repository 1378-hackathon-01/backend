using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class UserAdminAuthStorage
{
    private readonly DatabaseContextFactory _dbContextFactory;

    internal UserAdminAuthStorage(DatabaseContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<UserAdminAuth>> Get(
        UserAdmin user,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context
            .AdminUserAuths
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var models = entities
            .Select(x => x.GetModel())
            .ToList();

        return models;
    }

    public async Task<UserAdminAuth?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .AdminUserAuths
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task Delete(
        UserAdminAuth auth,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(auth);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserAdminAuthEntity(auth);
        context.AdminUserAuths.Attach(entity);
        context.AdminUserAuths.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert(
        UserAdminAuth auth,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(auth);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserAdminAuthEntity(auth);
        context.AdminUserAuths.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
