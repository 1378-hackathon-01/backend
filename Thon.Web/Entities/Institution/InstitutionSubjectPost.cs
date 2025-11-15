namespace Thon.Web.Entities.Institution;

public class InstitutionSubjectPost(string title, string abbreviation)
{
    public string Title { get; } = title;

    public string Abbreviation { get; } = abbreviation;
}
