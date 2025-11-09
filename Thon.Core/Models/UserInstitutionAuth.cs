namespace Thon.Core.Models;

/// <summary>
/// Авторизация пользователя-сотрудника учебного заведения.
/// </summary>
public class UserInstitutionAuth : BaseModel
{
    /// <summary>
    /// ID сотрудника учебного заведения, которому принадлежит авторизация.
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
    /// <param name="user">Пользователь.</param>
    public UserInstitutionAuth(UserInstitution user)
    {
        UserId = user.Id;
    }

    /// <summary>
    /// Копироваие авторизации.
    /// </summary>
    /// <param name="model">Копируемая авторизация.</param>
    public UserInstitutionAuth(UserAdminAuth model) : base(model)
    {
        UserId = model.UserId;
        DeviceUserAgent = model.DeviceUserAgent;
        DeviceIpAddress = model.DeviceIpAddress;
    }

    /// <summary>
    /// Восстановление авторизации.
    /// </summary>
    public UserInstitutionAuth(
        Guid id,
        Guid userId,
        string? deviceUserAgent,
        string? deviceIpAddress,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        UserId = userId;
        DeviceUserAgent = deviceUserAgent;
        DeviceIpAddress = deviceIpAddress;
    }
}
