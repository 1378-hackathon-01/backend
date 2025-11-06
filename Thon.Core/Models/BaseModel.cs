namespace Thon.Core.Models;

/// <summary>
/// Базовая сущность системы.
/// </summary>
public class BaseModel
{
    /// <summary>
    /// ID объекта.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Дата создания объекта.
    /// </summary>
    public DateTime CreatedAtUtc { get; }

    /// <summary>
    /// Дата последнего изменения (копирования) объекта.
    /// </summary>
    public DateTime UpdatedAtUtc { get; init; }

    /// <summary>
    /// Конструктор создания нового объекта.
    /// </summary>
    public BaseModel()
    {
        Id = Guid.NewGuid();
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Конструктор копирования объекта.
    /// </summary>
    /// <param name="model">Копируемая сущность.</param>
    public BaseModel(BaseModel model)
    {
        Id = model.Id;
        CreatedAtUtc = model.CreatedAtUtc;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Конструктор восстановления объекта.
    /// </summary>
    /// <param name="id">ID объекта.</param>
    /// <param name="createdAtUtc">Дата создания объекта.</param>
    /// <param name="updatedAtUtc">Дата обновления объекта.</param>
    public BaseModel(
        Guid id,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }
}
