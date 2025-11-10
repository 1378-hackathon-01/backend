using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentEntityConfiguration : BaseModelEntityConfiguration<StudentEntity>
{
    public new void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<InstitutionEntity>()
            .WithMany()
            .HasForeignKey(x => x.InstitutionId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasIndex(x => new { x.InstitutionId, x.CreatedAtUtc });

        builder
            .HasIndex(x => new { x.VkId, x.CreatedAtUtc });

        builder
            .HasIndex(x => x.VkId)
            .IsUnique();

        builder
            .HasIndex(x => new { x.MaxId, x.CreatedAtUtc });

        builder
            .HasIndex(x => x.MaxId)
            .IsUnique();

        builder
            .HasIndex(x => new { x.TelegramId, x.CreatedAtUtc });

        builder
            .HasIndex(x => x.TelegramId)
            .IsUnique();
    }
}