using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class BaseModelEntity : IEntity<BaseModel>
{
    public Guid Id { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }

    public BaseModelEntity(
        Guid id,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
    {
        Id = id;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }

    public BaseModelEntity(BaseModel model)
    {
        Id = model.Id;
        CreatedAtUtc = model.CreatedAtUtc;
        UpdatedAtUtc = model.UpdatedAtUtc;
    }

    public BaseModel GetModel()
    {
        return new BaseModel(
            id: Id,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
