using Thon.App.Exceptions;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class SubjectService(StorageService storage)
{
    public async Task<IReadOnlyList<Subject>> Get(
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(institution);

        var subjects = await storage.Subjects
            .Get( 
                institution: institution,
                cancellationToken: cancellationToken);
       
        return subjects;
    }

    public async Task<IReadOnlyList<Subject>> Get(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);

        var groupSubjects = await storage.GroupSubjects
            .Get(
                group: group,
                cancellationToken: cancellationToken);

        var subjects = new List<Subject>();

        foreach (var groupSubject in groupSubjects)
        {
            var subject = await storage.Subjects.Get(
                id: groupSubject.SubjectId,
                cancellationToken: cancellationToken);

            if (subject is null)
                throw new NullReferenceException();

            subjects.Add(subject);
        }

        return subjects;
    }

    public async Task<Subject?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var subjects = await storage.Subjects
            .Get(
                id: id,
                cancellationToken: cancellationToken);

        return subjects;
    }

    public async Task<Subject> Create(
        Institution institution, 
        string title,
        string abbreviation,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(institution);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(title);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(abbreviation);

        title = title.Trim();
        abbreviation = abbreviation.Trim();

        var subject = new Subject(
            institution: institution,
            abbreviation: abbreviation,
            title: title);

        await storage.Subjects
            .Insert(
                subject: subject,
                cancellationToken: cancellationToken);

        return subject;
    }

    public async Task Delete(
        Subject subject,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(subject);

        await storage.Subjects
            .Delete(
                subject: subject,
                cancellationToken: cancellationToken);
    }
    
    public async Task<GroupSubject> Add(
        Group group,
        Subject subject,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);
        ThonArgumentException.ThrowIfNull(subject);

        var groupSubjects = await storage.GroupSubjects.Get(group, cancellationToken);

        if (groupSubjects.Any(x => x.SubjectId == subject.Id))
            throw new ThonConflictException();

        var groupSubject = new GroupSubject(group, subject);

        await storage.GroupSubjects.Insert(groupSubject, cancellationToken);

        return groupSubject;
    }

    public async Task<GroupSubject?> GetGroupSubject(
        Group group,
        Subject subject,
        CancellationToken cancellationToken = default)
    {
        var groupSubjects = await storage.GroupSubjects.Get(group, cancellationToken);
        return groupSubjects.FirstOrDefault(x => x.SubjectId == subject.Id);
    }

    public async Task<GroupSubject> Update(
        Group group,
        Subject subject,
        string? content = null,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);
        ThonArgumentException.ThrowIfNull(subject);

        var groupSubjects = await storage.GroupSubjects.Get(group, cancellationToken);
        var groupSubject = groupSubjects.FirstOrDefault(x => x.SubjectId == subject.Id);

        if (groupSubject is null)
            throw new ThonNotFoundException();

        if (content != null)
        {
            var trimmedText = content.Trim();

             groupSubject = new GroupSubject(groupSubject) 
             { 
                 Content = trimmedText.Length != 0 ? trimmedText : null 
             };
        }

        await storage.GroupSubjects.Update(groupSubject, cancellationToken);
        return groupSubject;
    }

    public async Task Remove(Group group, Subject subject, CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);
        ThonArgumentException.ThrowIfNull(subject);

        var groupSubjects = await storage.GroupSubjects.Get(group, cancellationToken);
        var groupSubject = groupSubjects.FirstOrDefault(x => x.SubjectId == subject.Id);

        if (groupSubject is null)
            throw new ThonNotFoundException();

        await storage.GroupSubjects.Delete(groupSubject, cancellationToken);
    } 
}
