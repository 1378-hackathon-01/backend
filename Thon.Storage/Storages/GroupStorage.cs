using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;
using Thon.Storage.Models;

namespace Thon.Storage.Storages;

public class GroupStorage
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    internal GroupStorage(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Group?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .Groups
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<IReadOnlyList<Group>> Get(
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context.Groups
            .AsNoTracking()
            .Where(x => x.FacultyId == faculty.Id)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return entities
            .Select(x => x.GetModel())
            .ToList();
    }

    public async Task Delete(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(group);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new GroupEntity(group);
        context.Groups.Attach(entity);
        context.Groups.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Insert(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(group);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new GroupEntity(group);
        context.Groups.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<StudentRequestInstitutionFacultyGroup>> GetRequests(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(group);

        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context
            .StudentRequestInstitutionFacultyGroups
            .AsNoTracking()
            .Where(x => x.GroupId == group.Id)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var models = entities
            .Select(x => x.GetModel())
            .ToList();

        return models;
    }

    public async Task<IReadOnlyList<StudentApproved>> GetApproves(
       Group group,
       CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(group);

        using var context = _dbContextFactory.CreateDbContext();

        var entities = await context
            .StudentsApproved
            .AsNoTracking()
            .Where(x => x.GroupId == group.Id)
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var models = entities
            .Select(x => x.GetModel())
            .ToList();

        return models;
    }

    public async Task<StudentRequestChain> GetRequestChain(
        StudentRequestInstitutionFacultyGroup studentRequestGroup,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(studentRequestGroup);

        using var context = _dbContextFactory.CreateDbContext();

        var studentRequestFaculty = await context
            .StudentRequestInstitutionsFaculties
            .AsNoTracking()
            .SingleAsync(x => x.Id == studentRequestGroup.StudentRequestInstitutionFacultyId, cancellationToken);

        var studentRequestInstitution = await context
            .StudentRequestInstitutions
            .AsNoTracking()
            .SingleAsync(x => x.Id == studentRequestFaculty.StudentRequestInstitutionId, cancellationToken);

        return new StudentRequestChain(
            studentRequestInstitution.GetModel(),
            studentRequestFaculty.GetModel(),
            studentRequestGroup);
    }
}
