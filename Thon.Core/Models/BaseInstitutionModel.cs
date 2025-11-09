namespace Thon.Core.Models;

/// <summary>
/// Сущность, привязанная к учебному заведению.
/// </summary>
public class BaseInstitutionModel : BaseModel
{
    /// <summary>
    /// ID учебного заведения, к которому относится сущность.
    /// </summary>
    public Guid InstitutionId { get; }

    /// <summary>
    /// Создание новой сущности, привязанной к учебному заведению.
    /// </summary>
    /// <param name="institution">Учебное заведение.</param>
    public BaseInstitutionModel(Institution institution)
    {
        InstitutionId = institution.Id;
    }

    /// <summary>
    /// Копирование сущности, привязанной к учебному заведению.
    /// </summary>
    /// <param name="model"></param>
    public BaseInstitutionModel(BaseInstitutionModel model) : base(model)
    {
        InstitutionId = model.InstitutionId;
    }

    /// <summary>
    /// Восстановление сущности, привязанной к учебному заведению.
    /// </summary>
    public BaseInstitutionModel(
        Guid id,
        Guid institutionId,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        InstitutionId = institutionId;
    }
}
