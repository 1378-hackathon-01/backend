
namespace Thon.Core.Models;

/// <summary>
/// Данные API-токена для доступа к системам.
/// </summary>
public class ApiToken : BaseModel
{
    /// <summary>
    /// Хеш API-токена.
    /// </summary>
    public string TokenHash { get; }

    /// <summary>
    /// Создание нового токена.
    /// </summary>
    public ApiToken(string tokenHash)
    {
        TokenHash = tokenHash;
    }

    /// <summary>
    /// Копирование токена.
    /// </summary>
    public ApiToken(ApiToken model) : base(model)
    {
        TokenHash = model.TokenHash;
    }

    /// <summary>
    /// Восстановление токена.
    /// </summary>
    public ApiToken(
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
}
