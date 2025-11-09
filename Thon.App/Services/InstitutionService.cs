using Thon.App.Exceptions;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class InstitutionService(StorageService storage)
{
    public async Task<IReadOnlyList<Institution>> Get(CancellationToken cancellationToken = default)
    {
        var institutions = await storage.Institutions.Get(cancellationToken);

        return institutions;
    }

    public async Task<Institution?> Get(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var institution = await storage.Institutions
            .Get(
                id: id, 
                cancellationToken: cancellationToken);

        return institution;
    }

    public async Task<Institution> Create(
        string title,
        string abbreviation,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(title);
        ThonArgumentException.ThrowIfNullOrWhiteSpace(abbreviation);

        title = title.Trim();
        abbreviation = abbreviation.Trim();

        var institution = new Institution(
            title: title,
            abbreviation: abbreviation);

        await storage.Institutions
            .Insert(
                institution: institution,
                cancellationToken: cancellationToken);

        return institution;
    }

    public async Task Delete(
        Institution institution,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(institution);

        await storage.Institutions.Delete(
            institution: institution,
            cancellationToken: cancellationToken);
    }
}
