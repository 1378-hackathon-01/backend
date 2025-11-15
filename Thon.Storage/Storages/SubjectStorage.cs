using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class SubjectStorage
{
    private readonly DatabaseContextFactory _dbContextFactory;

    internal SubjectStorage(DatabaseContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<Subject>> Get(
        Institution institution, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(institution);

        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context
            .Subjects
            .AsNoTracking()
            .Where(x => x.InstitutionId == institution.Id)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return entities
            .Select(x => x.GetModel())
            .ToList();
    }

    public async Task<Subject?> Get(
       Guid id,
       CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .Subjects
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task Insert(
        Subject subject,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subject);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new SubjectEntity(subject);
        context.Subjects.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(
       Subject subject,
       CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subject);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new SubjectEntity(subject);
        context.Subjects.Attach(entity);
        context.Subjects.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
