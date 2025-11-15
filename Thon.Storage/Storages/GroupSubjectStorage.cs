using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class GroupSubjectStorage
{
    private readonly DatabaseContextFactory _dbContextFactory;

    internal GroupSubjectStorage(DatabaseContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<GroupSubject>> Get(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(group);

        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context
            .GroupSubjects
            .AsNoTracking()
            .Where(x => x.GroupId == group.Id)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return entities
            .Select(x => x.GetModel())
            .ToList();
    }

    public async Task<GroupSubject?> Get(
       Guid id,
       CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .GroupSubjects
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task Insert(
        GroupSubject groupSubject,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(groupSubject);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new GroupSubjectEntity(groupSubject);
        context.GroupSubjects.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(
        GroupSubject groupSubject,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(groupSubject);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new GroupSubjectEntity(groupSubject);
        context.GroupSubjects.Attach(entity);
        context.GroupSubjects.Update(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(
        GroupSubject groupSubject,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(groupSubject);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new GroupSubjectEntity(groupSubject);
        context.GroupSubjects.Attach(entity);
        context.GroupSubjects.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
