using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class UserInstitutionStorage
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    internal UserInstitutionStorage(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<UserInstitution?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .InstitutionUsers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<UserInstitution?> Get(
        string login,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .InstitutionUsers
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Login == login, cancellationToken);

        var model = entity?.GetModel();

        return model;
    }

    public async Task Insert(
        UserInstitution user,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserInstitutionEntity(user);
        context.InstitutionUsers.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
