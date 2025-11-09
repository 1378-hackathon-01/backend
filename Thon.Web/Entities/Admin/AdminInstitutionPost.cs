namespace Thon.Web.Entities.Admin;

public class AdminInstitutionPost(string title, string abbreviation)
{
    public string Title { get; } = title;

    public string Abbreviation { get; } = abbreviation;
}
