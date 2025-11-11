namespace Thon.Web.Entities.Institution;

public class InstitutionFacultyPost(string title, string abbreviation)
{
    public string Title { get; } = title;

    public string Abbreviation { get; } = abbreviation;
}
