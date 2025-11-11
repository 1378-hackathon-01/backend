namespace Thon.Core.Models;

public class StudentInstitutionFaculty : BaseModel
{
    public Guid StudentInstitutionId { get; }

    public Guid FacultyId { get; }

    public StudentInstitutionFaculty(
        StudentInstitution studentInstitution,
        Faculty faculty)
    {
        StudentInstitutionId = studentInstitution.Id;
        FacultyId = faculty.Id;
    }

    public StudentInstitutionFaculty(StudentInstitutionFaculty model) : base(model)
    {
        StudentInstitutionId = model.StudentInstitutionId;
        FacultyId = model.FacultyId;
    }

    public StudentInstitutionFaculty(
        Guid id,
        Guid studentInstitutionId,
        Guid facultyId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc, 
            updatedAtUtc: updatedAtUtc)
    {
        StudentInstitutionId = studentInstitutionId;
        FacultyId = facultyId;
    }
}
