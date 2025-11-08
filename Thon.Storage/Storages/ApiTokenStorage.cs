using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class ApiTokenStorage
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    internal ApiTokenStorage(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<ApiToken>> Get(CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context.ApiTokens
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return entities
            .Select(x => x.GetModel())
            .ToList();
    }

    public async Task<ApiToken?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context.ApiTokens
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);

        return entity?.GetModel();
    }

    public async Task Insert(
        ApiToken model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        using var context = _dbContextFactory.CreateDbContext();
        
        var entity = new ApiTokenEntity(model);
        context.ApiTokens.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(
        ApiToken model,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(model);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new ApiTokenEntity(model);

        context.ApiTokens.Attach(entity);
        context.ApiTokens.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
