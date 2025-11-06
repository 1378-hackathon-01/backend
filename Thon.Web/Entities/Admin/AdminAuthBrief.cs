namespace Thon.Web.Entities.Admin;

public class AdminAuthBrief(string token)
{
    public string Token { get; } = token;
}
