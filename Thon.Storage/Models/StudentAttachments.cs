using Thon.Core.Models;

namespace Thon.Storage.Models;

public class StudentAttachments
{
    public StudentInstitution? Institution { get; init; } 

    public StudentInstitutionFaculty? Faculty { get; init; }
}
