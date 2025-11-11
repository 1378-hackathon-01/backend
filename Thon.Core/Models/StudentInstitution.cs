namespace Thon.Core.Models;

public class StudentInstitution : BaseModel
{
    public Guid StudentId { get; }

    public Guid InstitutionId { get; }

    public StudentInstitution(
        Student student, 
        Institution institution)
    {
        StudentId = student.Id;
        InstitutionId = institution.Id;
    }

    public StudentInstitution(StudentInstitution model) : base(model)
    {
        StudentId = model.StudentId;
        InstitutionId = model.InstitutionId;
    }

    public StudentInstitution(
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
