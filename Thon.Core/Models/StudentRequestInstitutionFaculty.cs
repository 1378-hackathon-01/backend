namespace Thon.Core.Models;

public class StudentRequestInstitutionFaculty : BaseModel
{
    public Guid StudentRequestInstitutionId { get; }

    public Guid FacultyId { get; }

    public StudentRequestInstitutionFaculty(
        StudentRequestInstitution studentRequestInstitution,
        Faculty faculty)
    {
        StudentRequestInstitutionId = studentRequestInstitution.Id;
        FacultyId = faculty.Id;
    }

    public StudentRequestInstitutionFaculty(StudentRequestInstitutionFaculty model) : base(model)
    {
        StudentRequestInstitutionId = model.StudentRequestInstitutionId;
        FacultyId = model.FacultyId;
    }

    public StudentRequestInstitutionFaculty(
        Guid id,
        Guid studentRequestInstitutionId,
        Guid facultyId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc, 
            updatedAtUtc: updatedAtUtc)
    {
        StudentRequestInstitutionId = studentRequestInstitutionId;
        FacultyId = facultyId;
    }
}
