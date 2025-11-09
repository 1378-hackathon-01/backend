using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class BaseInstitutionModelEntity : BaseModelEntity, IEntity<BaseInstitutionModel>
{
    public Guid InstitutionId { get; set; }

    public BaseInstitutionModelEntity(BaseInstitutionModel model) : base(model)
    {
        InstitutionId = model.InstitutionId;
    }

    public BaseInstitutionModelEntity(
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

    public new BaseInstitutionModel GetModel()
    {
        return new BaseInstitutionModel(
            id: Id,
            institutionId: InstitutionId,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
