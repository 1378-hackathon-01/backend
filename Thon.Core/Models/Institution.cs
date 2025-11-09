
namespace Thon.Core.Models;

/// <summary>
/// Учебное заведение.
/// </summary>
public class Institution : BaseModel
{
    /// <summary>
    /// Аббревиатура названия.
    /// </summary>
    public string Abbreviation { get; }

    /// <summary>
    /// Полное название.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Создание нового учебного заведения.
    /// </summary>
    /// <param name="abbreviation">Аббревиатура названия.</param>
    /// <param name="title">Полное название.</param>
    public Institution(
        string abbreviation,
        string title)
    {
        Abbreviation = abbreviation;
        Title = title;
    }

    /// <summary>
    /// Копирование учебного заведения.
    /// </summary>
    public Institution(Institution model) : base(model)
    {
        Abbreviation = model.Abbreviation;
        Title = model.Title;
    }

    /// <summary>
    /// Восстановление учебного заведения.
    /// </summary>
    public Institution(
        Guid id,
        string abbreviation,
        string title,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id, 
            createdAtUtc: createdAtUtc, 
            updatedAtUtc: updatedAtUtc)
    {
        Abbreviation = abbreviation;
        Title = title;
    }
}
