namespace Thon.Core.Models;

public class StudentRequestInstitution : BaseModel
{
    public Guid StudentId { get; }

    public Guid InstitutionId { get; }

    public StudentRequestInstitution(
        Student student, 
        Institution institution)
    {
        StudentId = student.Id;
        InstitutionId = institution.Id;
    }

    public StudentRequestInstitution(StudentRequestInstitution model) : base(model)
    {
        StudentId = model.StudentId;
        InstitutionId = model.InstitutionId;
    }

    public StudentRequestInstitution(
        Guid id,
        Guid studentId,
        Guid institutionId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        StudentId = studentId;
        InstitutionId = institutionId;
    }
}
