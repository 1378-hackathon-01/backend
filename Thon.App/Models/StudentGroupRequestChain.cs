using Thon.Core.Models;

namespace Thon.App.Models;

public class StudentGroupRequestChain(
    StudentRequestInstitution institution,
    StudentRequestInstitutionFaculty faculty,
    StudentRequestInstitutionFacultyGroup group)
{
    public StudentRequestInstitution Institution { get; } = institution;

    public StudentRequestInstitutionFaculty Faculty { get; } = faculty;

    public StudentRequestInstitutionFacultyGroup Group { get; } = group;
}
