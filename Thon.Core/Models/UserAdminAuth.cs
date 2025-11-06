namespace Thon.Core.Models;

/// <summary>
/// Авторизация администратора.
/// </summary>
public class UserAdminAuth : BaseModel
{
    /// <summary>
    /// ID администратора, которому принадлежит авторизация.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// User-Agent, с которого произведена авторизация.
    /// </summary>
    public string? DeviceUserAgent { get; init; }

    /// <summary>
    /// IP-адресс, с которого произведена авторизация.
    /// </summary>
    public string? DeviceIpAddress { get; init; }

    /// <summary>
    /// Создание новой авторизации пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    public UserAdminAuth(Guid userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Копироваие авторизации.
    /// </summary>
    /// <param name="model">Копируемая авторизация.</param>
    public UserAdminAuth(UserAdminAuth model) : base(model)
    {
        UserId = model.UserId;
        DeviceUserAgent = model.DeviceUserAgent;
        DeviceIpAddress = model.DeviceIpAddress;
    }

    /// <summary>
    /// Восстановление авторизации.
    /// </summary>
    public UserAdminAuth(
        Guid id,
        Guid userId,
        string? deviceUserAgent,
        string? deviceIpAddress,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc:updatedAtUtc)
    {
        UserId = userId;
        DeviceUserAgent = deviceUserAgent;
        DeviceIpAddress = deviceIpAddress;
    }
}