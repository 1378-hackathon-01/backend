using Thon.App.Models;
using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiStudentSearch(StudentSearchResult student)
{
    public Guid Id { get; } = student.Id;
}
