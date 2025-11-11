using Thon.Storage.Storages;

namespace Thon.Storage;

public class StorageService
{
    public UserAdminStorage UserAdmins { get; }

    public UserAdminAuthStorage UserAdminAuths { get; }

    public ApiTokenStorage ApiTokens { get; }

    public InstitutionStorage Institutions { get; }

    public UserInstitutionStorage UserInstitutions { get; }

    public UserInstitutionAuthStorage UserInstitutionAuths { get; }

    public StudentStorage Students { get; }

    public FacultyStorage Faculties { get; }

    public StorageService(IStorageConfiguration configuration)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(configuration.ConnectionString);

        var dbContextFactory = new DatabaseContextFactory(configuration.ConnectionString);
        UserInstitutionAuths = new UserInstitutionAuthStorage(dbContextFactory);
        UserInstitutions = new UserInstitutionStorage(dbContextFactory);
        UserAdminAuths = new UserAdminAuthStorage(dbContextFactory);
        Institutions = new InstitutionStorage(dbContextFactory);
        UserAdmins = new UserAdminStorage(dbContextFactory);
        ApiTokens = new ApiTokenStorage(dbContextFactory);
        Faculties = new FacultyStorage(dbContextFactory);
        Students = new StudentStorage(dbContextFactory);
    }
}
