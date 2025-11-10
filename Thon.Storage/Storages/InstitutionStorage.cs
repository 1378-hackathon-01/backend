using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class InstitutionStorage
{
    private readonly DatabaseContextFactory _dbContextFactory;

    internal InstitutionStorage(DatabaseContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<Institution>> Get(CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context.Institutions
            .AsNoTracking()
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return entities
            .Select(x => x.GetModel())
            .ToList();
    }

    public async Task<Institution?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .Institutions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task Delete(
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(institution);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new InstitutionEntity(institution);
        context.Institutions.Attach(entity);
        context.Institutions.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert(
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(institution);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new InstitutionEntity(institution);
        context.Institutions.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
