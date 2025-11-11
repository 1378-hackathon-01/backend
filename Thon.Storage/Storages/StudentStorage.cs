using Microsoft.EntityFrameworkCore;
using Thon.Core.Models;
using Thon.Storage.Entities;
using Thon.Storage.Models;

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

    public async Task DeattachFromInstitution(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitution = await context
            .StudentInstitutions
            .AsTracking()
            .Where(x => x.StudentId == student.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentInstitutions
            .RemoveRange(studentInstitution);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeattachFromFaculty(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitution = await context
            .StudentInstitutions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.StudentId == student.Id, cancellationToken);

        if (studentInstitution is null)
            return;

        var studentInstitutionFaculties = await context
            .StudentInstitutionsFaculties
            .AsTracking()
            .Where(x => x.StudentInstitutionId == studentInstitution.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentInstitutionsFaculties
            .RemoveRange(studentInstitutionFaculties);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<StudentAttachments> GetAttachments(
        Student student, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitution = await context
            .StudentInstitutions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.StudentId == student.Id, cancellationToken);

        if (studentInstitution is not null)
        {
            var studentInstitutionFaculty = await context
                .StudentInstitutionsFaculties
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.StudentInstitutionId == studentInstitution.Id, cancellationToken);

            return new StudentAttachments
            {
                Institution = studentInstitution.GetModel(),
                Faculty = studentInstitutionFaculty?.GetModel(),
            };
        }

        return new StudentAttachments
        {
            Institution = studentInstitution?.GetModel()
        };
    }

    public async Task<StudentInstitution> Attach(
        Student student, 
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(institution);

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitutions = await context
            .StudentInstitutions
            .AsTracking()
            .Where(x => x.StudentId == student.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentInstitutions
            .RemoveRange(studentInstitutions);

        var model = new StudentInstitution(student, institution);
        var entity = new StudentInstitutionEntity(model);
        context.StudentInstitutions.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return model;
    }

    public async Task<StudentInstitutionFaculty> Attach(
        StudentInstitution studentInstitution,
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(studentInstitution);
        ArgumentNullException.ThrowIfNull(faculty);

        if (faculty.InstitutionId != studentInstitution.InstitutionId)
            throw new ArgumentException("Invalid Faculty!");

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitutionFaculties = await context
            .StudentInstitutionsFaculties
            .AsTracking()
            .Where(x => x.StudentInstitutionId == studentInstitution.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentInstitutionsFaculties
            .RemoveRange(studentInstitutionFaculties);

        var model = new StudentInstitutionFaculty(studentInstitution, faculty);
        var entity = new StudentInstitutionFacultyEntity(model);
        context.StudentInstitutionsFaculties.Add(entity);

        return model;
    }
}
