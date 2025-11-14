using Thon.Core.Models;

namespace Thon.Web.Entities.Institution;

public class InstitutionStudentApproveFull(Student student)
{
    public Guid Id { get; } = student.Id;

    public string? FullName { get; } = student.FullName;
}
