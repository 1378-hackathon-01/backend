using Thon.Core.Models;

namespace Thon.Storage.Models;

public class StudentRequestChain
{
    public StudentRequestInstitution Institution { get; } 

    public StudentRequestInstitutionFaculty Faculty { get; }

    public StudentRequestInstitutionFacultyGroup Group { get; }

    public StudentRequestChain(
        StudentRequestInstitution institution,
        StudentRequestInstitutionFaculty faculty,
        StudentRequestInstitutionFacultyGroup group)
    {
        Institution = institution;
        Faculty = faculty;
        Group = group;
    }
}
