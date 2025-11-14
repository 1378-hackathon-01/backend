namespace Thon.Core.Models;

public class StudentApproved : BaseModel
{
    public Guid InstitutionId { get; }

    public Guid FacultyId { get; }
    
    public Guid GroupId { get; }

    public Guid StudentId { get; }

    public StudentApproved(
        Institution institution,
        Faculty faculty,
        Group group,
        Student student)
    {
        InstitutionId = institution.Id;
        FacultyId = faculty.Id;
        GroupId = group.Id;
        StudentId = student.Id;
    }

    public StudentApproved(StudentApproved model) : base(model)
    {
        InstitutionId = model.InstitutionId;
        FacultyId = model.FacultyId;
        GroupId = model.GroupId;
        StudentId = model.StudentId;
    }

    public StudentApproved(
        Guid id, 
        Guid institutionId, 
        Guid facultyId, 
        Guid groupId, 
        Guid studentId,
        DateTime createdAtUtc, 
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        InstitutionId = institutionId;
        FacultyId = facultyId;
        GroupId = groupId;
        StudentId = studentId;
    }
}
