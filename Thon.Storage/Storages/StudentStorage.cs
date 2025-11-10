using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;

namespace Thon.Storage.Storages;

public class StudentStorage
{
    private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

    internal StudentStorage(IDbContextFactory<DatabaseContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Student?> Get(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context.Students
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<Student?> GetByTelegramId(
        long telegramId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(telegramId);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context.Students
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.TelegramId == telegramId, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<Student?> GetByMaxId(
        long maxId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxId);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context.Students
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.MaxId == maxId, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<Student?> GetByVkId(
        long vkId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(vkId);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context.Students
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.VkId == vkId, cancellationToken);

        return entity?.GetModel();
    }

    public async Task Insert(
        Student student, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new StudentEntity(student);
        context.Students.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(
        Student student, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new StudentEntity(student);
        context.Students.Attach(entity);
        context.Students.Update(entity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new StudentEntity(student);
        context.Students.Attach(entity);
        context.Students.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
