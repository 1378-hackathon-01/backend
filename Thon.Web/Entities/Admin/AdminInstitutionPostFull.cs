namespace Thon.Web.Entities.Admin;

public class AdminInstitutionPostFull(string login, string password)
{
    public string Login { get; } = login;

    public string Password { get; } = password;
}
