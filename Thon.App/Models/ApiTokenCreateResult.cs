namespace Thon.App.Models;

public class ApiTokenCreateResult(string token)
{
    public string Token { get; } = token;
}
