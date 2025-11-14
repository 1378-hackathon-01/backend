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

    public async Task Deattach(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var studentApproved = await context
            .StudentsApproved
            .Where(x => x.StudentId == student.Id)
            .ToArrayAsync(cancellationToken);

        context.StudentsApproved.RemoveRange(studentApproved);

        var studentInstitutions = await context
            .StudentRequestInstitutions
            .AsNoTracking()
            .Where(x => x.StudentId == student.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentRequestInstitutions
            .RemoveRange(studentInstitutions);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<StudentRequest> GetRequest(
        Student student, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitution = await context
            .StudentRequestInstitutions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.StudentId == student.Id, cancellationToken);

        if (studentInstitution is not null)
        {
            var studentInstitutionFaculty = await context
                .StudentRequestInstitutionsFaculties
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.StudentRequestInstitutionId == studentInstitution.Id, cancellationToken);

            if (studentInstitutionFaculty is not null)
            {
                var studentInstitutionFacultyGroup = await context
                    .StudentRequestInstitutionFacultyGroups
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.StudentRequestInstitutionFacultyId == studentInstitutionFaculty.Id, cancellationToken);

                return new StudentRequest
                {
                    Institution = studentInstitution.GetModel(),
                    Faculty = studentInstitutionFaculty.GetModel(),
                    Group = studentInstitutionFacultyGroup?.GetModel(),
                };
            }

            return new StudentRequest
            {
                Institution = studentInstitution.GetModel(),
                Faculty = studentInstitutionFaculty?.GetModel(),
            };
        }

        return new StudentRequest
        {
            Institution = studentInstitution?.GetModel()
        };
    }

    public async Task<StudentRequestInstitution> Attach(
        Student student, 
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(institution);

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitutions = await context
            .StudentRequestInstitutions
            .AsTracking()
            .Where(x => x.StudentId == student.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentRequestInstitutions
            .RemoveRange(studentInstitutions);

        var model = new StudentRequestInstitution(student, institution);
        var entity = new StudentRequestInstitutionEntity(model);
        context.StudentRequestInstitutions.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return model;
    }

    public async Task<StudentRequestInstitutionFaculty> Attach(
        StudentRequestInstitution studentInstitution,
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(studentInstitution);
        ArgumentNullException.ThrowIfNull(faculty);

        if (faculty.InstitutionId != studentInstitution.InstitutionId)
            throw new ArgumentException("Invalid Faculty!");

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitutionFaculties = await context
            .StudentRequestInstitutionsFaculties
            .AsTracking()
            .Where(x => x.StudentRequestInstitutionId == studentInstitution.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentRequestInstitutionsFaculties
            .RemoveRange(studentInstitutionFaculties);

        var model = new StudentRequestInstitutionFaculty(studentInstitution, faculty);
        var entity = new StudentRequestInstitutionFacultyEntity(model);
        context.StudentRequestInstitutionsFaculties.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return model;
    }

    public async Task<StudentRequestInstitutionFacultyGroup> Attach(
        StudentRequestInstitutionFaculty studentInstitutionFaculty,
        Group group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(studentInstitutionFaculty);
        ArgumentNullException.ThrowIfNull(group);

        if (group.FacultyId != studentInstitutionFaculty.FacultyId)
            throw new ArgumentException("Invalid Faculty!");

        using var context = _dbContextFactory.CreateDbContext();

        var studentInstitutionFacultyGroups = await context
            .StudentRequestInstitutionFacultyGroups
            .AsTracking()
            .Where(x => x.StudentRequestInstitutionFacultyId == studentInstitutionFaculty.Id)
            .ToListAsync(cancellationToken);

        context
            .StudentRequestInstitutionFacultyGroups
            .RemoveRange(studentInstitutionFacultyGroups);

        var model = new StudentRequestInstitutionFacultyGroup(studentInstitutionFaculty, group);
        var entity = new StudentRequestInstitutionFacultyGroupEntity(model);
        context.StudentRequestInstitutionFacultyGroups.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return model;
    }

    public async Task<StudentRequestChain?> GetRequestChain(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var studentRequestInstitution = await context
            .StudentRequestInstitutions
            .SingleOrDefaultAsync(x => x.StudentId == student.Id, cancellationToken);

        if (studentRequestInstitution is null) return null;

        var studentRequestFaculty = await context
            .StudentRequestInstitutionsFaculties
            .SingleOrDefaultAsync(x => x.StudentRequestInstitutionId == studentRequestInstitution.Id, cancellationToken);

        if (studentRequestFaculty is null) return null;

        var studentRequestGroup = await context
            .StudentRequestInstitutionFacultyGroups
            .SingleOrDefaultAsync(x => x.StudentRequestInstitutionFacultyId == studentRequestFaculty.Id, cancellationToken);

        if (studentRequestGroup is null) return null;

        return new StudentRequestChain(
            institution: studentRequestInstitution.GetModel(),
            faculty: studentRequestFaculty.GetModel(),
            group: studentRequestGroup.GetModel());
    }

    public async Task<StudentApproved> RequestApprove(
        Student student,
        StudentRequestInstitution institution,
        StudentRequestInstitutionFaculty faculty,
        StudentRequestInstitutionFacultyGroup group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(institution);
        ArgumentNullException.ThrowIfNull(faculty);
        ArgumentNullException.ThrowIfNull(group);

        if (institution.StudentId != student.Id)
            throw new ArgumentException();

        if (faculty.StudentRequestInstitutionId != institution.Id)
            throw new ArgumentException();

        if (group.StudentRequestInstitutionFacultyId != faculty.Id)
            throw new ArgumentException();

        using var context = _dbContextFactory.CreateDbContext();

        var studentAlreadyApproved = await context
            .StudentsApproved
            .AnyAsync(x => x.StudentId == student.Id, cancellationToken);

        if (studentAlreadyApproved)
            throw new InvalidOperationException("Student already approved!");

        var approveModel = new StudentApproved(
            student: student,
            institution: institution,
            faculty: faculty,
            group: group);

        var approveEntity = new StudentApprovedEntity(approveModel);
        context.StudentsApproved.Add(approveEntity);

        var institutionEntity = new StudentRequestInstitutionEntity(institution);
        context.StudentRequestInstitutions.Attach(institutionEntity);
        context.StudentRequestInstitutions.Remove(institutionEntity);

        await context.SaveChangesAsync(cancellationToken);

        return approveModel;
    }

    public async Task RequestDecline(
        Student student,
        StudentRequestInstitution institution,
        StudentRequestInstitutionFaculty faculty,
        StudentRequestInstitutionFacultyGroup group,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(institution);
        ArgumentNullException.ThrowIfNull(faculty);
        ArgumentNullException.ThrowIfNull(group);

        if (institution.StudentId != student.Id)
            throw new ArgumentException();

        if (faculty.StudentRequestInstitutionId != institution.Id)
            throw new ArgumentException();

        if (group.StudentRequestInstitutionFacultyId != faculty.Id)
            throw new ArgumentException();

        using var context = _dbContextFactory.CreateDbContext();

        var institutionEntity = new StudentRequestInstitutionEntity(institution);
        context.StudentRequestInstitutions.Attach(institutionEntity);
        context.StudentRequestInstitutions.Remove(institutionEntity);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<StudentApproved?> ApproveGet(
        Student student, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = await context
            .StudentsApproved
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.StudentId == student.Id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task ApproveDelete(
        StudentApproved studentApprove,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(studentApprove);

        using var context = _dbContextFactory.CreateDbContext();

        var entity = new StudentApprovedEntity(studentApprove);
        context.StudentsApproved.Attach(entity);
        context.StudentsApproved.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}
