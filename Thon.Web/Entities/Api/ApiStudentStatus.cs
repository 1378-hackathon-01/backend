namespace Thon.Web.Entities.Api;

public class ApiStudentStatus(bool approved)
{
    public bool Approved { get; } = approved;
}
