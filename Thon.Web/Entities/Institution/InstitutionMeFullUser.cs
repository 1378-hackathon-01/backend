using Thon.Core.Models;

namespace Thon.Web.Entities.Institution;

public class InstitutionMeFullUser(UserInstitution user)
{
    public Guid Id { get; } = user.Id;

    public string FullName { get; } = user.FullName;
}
