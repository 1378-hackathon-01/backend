using Thon.App.Exceptions;
using Thon.App.Models;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class StudentService(StorageService storage)
{
    public async Task<Student?> Get(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var student = await storage.Students
            .Get(
                id: id, 
                cancellationToken: cancellationToken);
        
        return student;
    }

    public async Task<Institution?> GetInstitution(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        var attachments = await storage
            .Students
            .GetAttachments(
                student: student,
                cancellationToken: cancellationToken);

        if (attachments.Institution is null)
            return null;

        var institution = await storage
            .Institutions
            .Get(
                id: attachments.Institution.InstitutionId,
                cancellationToken: cancellationToken);

        return institution;
    }

    public async Task<Faculty?> GetFaculty(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        var attachments = await storage
            .Students
            .GetAttachments(
                student: student,
                cancellationToken: cancellationToken);

        if (attachments.Faculty is null)
            return null;

        var faculty = await storage
            .Faculties
            .Get(
                id: attachments.Faculty.FacultyId,
                cancellationToken: cancellationToken);

        return faculty;
    }

    public async Task<StudentSearchResult?> GetByVk(
        long vkId,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNegativeOrZero(vkId);

        var student = await storage.Students
            .GetByVkId(
                vkId: vkId,
                cancellationToken: cancellationToken);

        var result = student is not null
            ? new StudentSearchResult(student)
            : null;

        return result;
    }

    public async Task<StudentSearchResult?> GetByMax(
       long maxId,
       CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNegativeOrZero(maxId);

        var student = await storage.Students
            .GetByMaxId(
                maxId: maxId,
                cancellationToken: cancellationToken);

        var result = student is not null
            ? new StudentSearchResult(student)
            : null;

        return result;
    }

    public async Task<StudentSearchResult?> GetByTelegram(
       long telegramId,
       CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNegativeOrZero(telegramId);

        var student = await storage.Students
            .GetByTelegramId(
                telegramId: telegramId,
                cancellationToken: cancellationToken);

        var result = student is not null
            ? new StudentSearchResult(student)
            : null;

        return result;
    }

    public async Task<Student> Create(
        long? vkId = null,
        long? maxId = null,
        long? telegramId = null,
        string? fullName = null,
        CancellationToken cancellationToken = default)
    {
        if (vkId is not null) 
            await ValidateVkId(vkId.Value, cancellationToken: cancellationToken);

        if (maxId is not null) 
            await ValidateMaxId(maxId.Value, cancellationToken: cancellationToken);

        if (telegramId is not null) 
            await ValidateTelegramId(telegramId.Value, cancellationToken: cancellationToken);

        fullName = fullName?.Trim();

        var student = new Student
        {
            FullName = fullName?.Length == 0 ? null : fullName,
            TelegramId = telegramId,
            MaxId = maxId,
            VkId = vkId,
        };

        await storage.Students
            .Insert(
                student: student, 
                cancellationToken: cancellationToken);

        return student;
    }

    public async Task<Student> Update(
        Student student,
        long? vkId = null,
        long? maxId = null,
        long? telegramId = null,
        string? fullName = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (vkId is not null)
            await ValidateVkId(vkId.Value, student, cancellationToken);

        if (maxId is not null)
            await ValidateMaxId(maxId.Value, student, cancellationToken);

        if (telegramId is not null)
            await ValidateTelegramId(telegramId.Value, student, cancellationToken);;
        
        fullName = fullName?.Trim().Length == 0 ? null : fullName?.Trim();

        student = new Student(student)
        {
            VkId = vkId,
            MaxId = maxId,
            TelegramId = telegramId,
            FullName = fullName,
        };

        await storage.Students.Update(student, cancellationToken);
        return student;
    }

    public async Task Delete(
        Student student, 
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(student);

        await storage.Students.Delete(student, cancellationToken);
    }

    public async Task AttachToInstitution(
        Student student,
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(student);
        ThonArgumentException.ThrowIfNull(institution);

        var attachments = await storage.Students.GetAttachments(student, cancellationToken);

        if (attachments.Institution is not null)
            throw new ThonConflictException("User already attached to the institution, please deattach firstly!");

        await storage.Students.Attach(student, institution, cancellationToken);
    }

    public async Task AttachToFaculty(
        Student student,
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(student);
        ThonArgumentException.ThrowIfNull(faculty);

        var attachments = await storage.Students.GetAttachments(student, cancellationToken);

        if (attachments.Faculty is not null)
            throw new ThonConflictException("User already attached to the institution, please deattach firstly!");

        if (attachments.Institution is null)
            throw new ThonArgumentException("Attach user to the institution firstly!");

        if (faculty.InstitutionId != attachments.Institution.InstitutionId)
            throw new ThonArgumentException("Invalid faculty for that institution!");

        await storage.Students.Attach(attachments.Institution, faculty, cancellationToken);
    }

    public async Task DeattachFromInstitution(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(student);

        await storage.Students.DeattachFromInstitution(student, cancellationToken);
    }

    public async Task DeattachFromFaculty(
        Student student,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(student);

        await storage.Students.DeattachFromFaculty(student, cancellationToken);
    }

    private async Task ValidateVkId(long vkId, Student? student = null, CancellationToken cancellationToken = default)
    {
        if (student is null)
            ThonArgumentException.ThrowIfNegativeOrZero(vkId, "VK ID can't be less or equal zero!");
        else
            ThonArgumentException.ThrowIfNegativeOrZero(vkId, "VK ID can't be negative!");

        if (student is not null && (vkId == 0 || student.VkId == vkId)) return;

        var foundStudent = await GetByVk(vkId, cancellationToken);

        if (foundStudent is not null) 
            throw new ThonConflictException("Student with that VK ID already exists!");
    }

    private async Task ValidateMaxId(long maxId, Student? student = null, CancellationToken cancellationToken = default)
    {
        if (student is null)
            ThonArgumentException.ThrowIfNegativeOrZero(maxId, "MAX ID can't be less or equal zero!");
        else
            ThonArgumentException.ThrowIfNegative(maxId, "MAX ID can't be negative!");

        if (student is not null && (maxId == 0 || student.MaxId == maxId)) return;

        var foundStudent = await GetByMax(maxId, cancellationToken);

        if (foundStudent is not null) 
            throw new ThonConflictException("Student with that MAX ID already exists!");
    }

    private async Task ValidateTelegramId(long telegramId, Student? student = null, CancellationToken cancellationToken = default)
    {
        if (student is null)
            ThonArgumentException.ThrowIfNegativeOrZero(telegramId, "Telegram ID can't be less or equal zero!");
        else
            ThonArgumentException.ThrowIfNegative(telegramId, "Telegram ID can't be negative!");

        if (student is not null && (telegramId == 0 || student.TelegramId == telegramId)) return;

        var foundStudent = await GetByTelegram(telegramId, cancellationToken);

        if (foundStudent is not null)
            throw new ThonConflictException("Student with that Telegram ID already exists!");
    }
}
