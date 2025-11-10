using Thon.Core.Models;

namespace Thon.Web.Entities.Api;

public class ApiStudent(Student student)
{
    public Guid Id { get; } = student.Id;

    public string? FullName { get; } = student.FullName;

    public long? VkId { get; } = student.VkId;

    public long? MaxId { get; } = student.MaxId;

    public long? TelegramId { get; } = student.TelegramId;
}
