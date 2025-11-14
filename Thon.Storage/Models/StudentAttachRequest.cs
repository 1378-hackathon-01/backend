using Thon.Core.Models;

namespace Thon.Storage.Models;

public class StudentAttachRequest
{
    public StudentRequestInstitution? Institution { get; init; } 

    public StudentRequestInstitutionFaculty? Faculty { get; init; }

    public StudentRequestInstitutionFacultyGroup? Group { get; init; }
}
