using Thon.Core.Models;

using InstitutionModel = Thon.Core.Models.Institution;

namespace Thon.Web.Entities.Institution;

public class InstitutionMeFull(UserInstitution user, InstitutionModel institution)
{
    public InstitutionMeFullUser User { get; } = new InstitutionMeFullUser(user);

    public InstitutionMeFullInstitution Institution { get; } = new InstitutionMeFullInstitution(institution);
}
