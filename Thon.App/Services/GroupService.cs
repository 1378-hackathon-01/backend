using Thon.App.Exceptions;
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
}
