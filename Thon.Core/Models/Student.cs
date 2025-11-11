namespace Thon.Core.Models;

/// <summary>
/// Студент - рядовой пользователь сервиса.
/// </summary>
public class Student : BaseModel
{
    /// <summary>
    /// Полное имя студента.
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// ID студента в системе мессенджера MAX.
    /// </summary>
    public long? MaxId { get; init; }

    /// <summary>
    /// ID студента в Telegram.
    /// </summary>
    public long? TelegramId { get; init; }

    /// <summary>
    /// ID студента во Вконтакте.
    /// </summary>
    public long? VkId { get; init; }

    public Student() { }

    public Student(Student model) : base(model)
    {
        VkId = model.VkId;
        MaxId = model.MaxId;
        TelegramId = model.TelegramId;
        FullName = model.FullName;
    }

    public Student(
        Guid id,
        long? vkId,
        long? maxId,
        long? telegramId,
        string? fullName,
        DateTime createdAtUtc,
        DateTime updatedAtUtc) 
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc:updatedAtUtc)
    {
        VkId = vkId;
        MaxId = maxId;
        TelegramId = telegramId;
        FullName = fullName;
    }
}
