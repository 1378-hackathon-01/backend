using Thon.App.Exceptions;
using Thon.App.Models;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class GroupService(StorageService storage)
{
    public async Task<IReadOnlyList<Group>> Get(
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(faculty);

        var faculties = await storage.Groups
            .Get(
                faculty: faculty,
                cancellationToken: cancellationToken);

        return faculties;
    }

    public async Task<Group?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var faculty = await storage.Groups
            .Get(
                id: id,
                cancellationToken: cancellationToken);

        return faculty;
    }

    public async Task<Group> Create(
        Faculty faculty,
        string title,
        string abbreviation,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(faculty);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(title);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(abbreviation);

        title = title.Trim();
        abbreviation = abbreviation.Trim();

        var group = new Group(
            faculty: faculty,
            abbreviation: abbreviation,
            title: title);

        await storage.Groups
            .Insert(
                group: group,
                cancellationToken: cancellationToken);

        return group;
    }

    public async Task Delete(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);

        await storage.Groups
            .Delete(
                group: group,
                cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<StudentGroupRequestChain>> GetRequests(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);

        var studentRequestsGroup = await storage
            .Groups.GetRequests(
                group: group,
                cancellationToken: cancellationToken);

        var chains = new List<StudentGroupRequestChain>();

        foreach (var studentRequestGroup in studentRequestsGroup)
        {
            var chain = await storage.Groups.GetRequestChain(
                studentRequestGroup,
                cancellationToken);

            chains.Add(new StudentGroupRequestChain(
                institution: chain.Institution,
                faculty: chain.Faculty,
                group: chain.Group));
        }

        return chains;
    }

    public async Task<IReadOnlyList<StudentApproved>> GetApproves(
        Group group,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(group);

        var approves = await storage
            .Groups
            .GetApproves(
                group: group,
                cancellationToken: cancellationToken);

        return approves;
    }
}
