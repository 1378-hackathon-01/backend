using Thon.App.Exceptions;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class FacultyService(StorageService storage)
{
    public async Task<IReadOnlyList<Faculty>> Get(
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(institution);

        var faculties = await storage.Faculties
            .Get( 
                institution: institution,
                cancellationToken: cancellationToken);
       
        return faculties;
    }

    public async Task<Faculty?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var faculty = await storage.Faculties
            .Get(
                id: id,
                cancellationToken: cancellationToken);

        return faculty;
    }

    public async Task<Faculty> Create(
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

        var faculty = new Faculty(
            institution: institution,
            abbreviation: abbreviation,
            title: title);

        await storage.Faculties
            .Insert(
                faculty: faculty,
                cancellationToken: cancellationToken);

        return faculty;
    }

    public async Task Delete(
        Faculty faculty,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(faculty);

        await storage.Faculties
            .Delete(
                faculty: faculty,
                cancellationToken: cancellationToken);
    }
}
