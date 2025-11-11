using Thon.Core.Models;

namespace Thon.Storage.Entities;

internal class StudentEntity : BaseModelEntity, IEntity<Student>
{
    public string? FullName { get; set; }

    public long? MaxId { get; set; }

    public long? TelegramId { get; set; }

    public long? VkId { get; set; }

    public StudentEntity(Student model) : base(model)
    {
        VkId = model.VkId;
        MaxId = model.MaxId;
        TelegramId = model.TelegramId;
        FullName = model.FullName;
    }

    public StudentEntity(
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
            updatedAtUtc: updatedAtUtc)
    {
        VkId = vkId;
        MaxId = maxId;
        TelegramId = telegramId;
        FullName = fullName;
    }

    public new Student GetModel()
    {
        return new Student(
            id: Id,
            vkId: VkId,
            maxId: MaxId,
            telegramId: TelegramId,
            fullName: FullName,
            createdAtUtc: CreatedAtUtc,
            updatedAtUtc: UpdatedAtUtc);
    }
}
