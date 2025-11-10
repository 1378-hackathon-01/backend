using Thon.Core.Models;

namespace Thon.App.Models;

public class StudentSearchResult(Student student)
{
    /// <summary>
    /// ID найденного студента.
    /// </summary>
    public Guid Id { get; } = student.Id;
}
