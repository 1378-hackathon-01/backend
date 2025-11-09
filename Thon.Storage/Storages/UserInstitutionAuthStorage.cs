using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class UserInstitutionAuthStorage
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    internal UserInstitutionAuthStorage(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<UserInstitutionAuth?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .InstitutionUserAuths
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task Delete(
        UserInstitutionAuth auth,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(auth);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserInstitutionAuthEntity(auth);
        context.InstitutionUserAuths.Attach(entity);
        context.InstitutionUserAuths.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert(
        UserInstitutionAuth auth,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(auth);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new UserInstitutionAuthEntity(auth);
        context.InstitutionUserAuths.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
