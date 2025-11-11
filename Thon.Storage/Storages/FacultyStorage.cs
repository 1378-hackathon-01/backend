using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class FacultyStorage
{
    private readonly DatabaseContextFactory _dbContextFactory;

    internal FacultyStorage(DatabaseContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Faculty?> Get(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .Faculties
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<IReadOnlyList<Faculty>> Get(
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context.Faculties
            .AsNoTracking()
            .Where(x => x.InstitutionId == institution.Id)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return entities
            .Select(x => x.GetModel())
            .ToList();
    }

    public async Task Delete(
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(faculty);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new FacultyEntity(faculty);
        context.Faculties.Attach(entity);
        context.Faculties.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert(
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(faculty);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new FacultyEntity(faculty);
        context.Faculties.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
