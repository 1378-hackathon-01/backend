using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class ApiTokenEntity : BaseModelEntity, IEntity<ApiToken>
{
    public string TokenHash { get; set; }

    public ApiTokenEntity(
        Guid id,
        string tokenHash,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        TokenHash = tokenHash;
    }

    public ApiTokenEntity(ApiToken model) : base(model)
    {
        TokenHash = model.TokenHash;
    }

    public new ApiToken GetModel()
    {
        return new ApiToken(
            id: Id,
            tokenHash: TokenHash,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
