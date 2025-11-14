namespace Thon.Core.Models;

public class Group : BaseModel
{
    public Guid FacultyId { get; }

    public string Title { get;  }

    public string Abbreviation { get; }

    public Group(
        Faculty faculty,
        string title,
        string abbreviation)
    {
        FacultyId = faculty.Id;
        Title = title;
        Abbreviation = abbreviation;
    }

    public Group(Group model) : base(model)
    {
        FacultyId = model.FacultyId;
        Title = model.Title;
        Abbreviation = model.Abbreviation;
    }

    public Group(
        Guid id,
        Guid facultyId,
        string title,
        string abbreviation,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        FacultyId = facultyId;
        Title = title;
        Abbreviation = abbreviation;
    }

}
