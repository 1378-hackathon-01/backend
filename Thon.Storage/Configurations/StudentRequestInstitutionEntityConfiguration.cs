using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentRequestInstitutionEntityConfiguration : BaseModelEntityConfiguration<StudentRequestInstitutionEntity>, IEntityTypeConfiguration<StudentRequestInstitutionEntity>
{
    public new void Configure(EntityTypeBuilder<StudentRequestInstitutionEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<InstitutionEntity>()
            .WithMany()
            .HasForeignKey(x => x.InstitutionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.InstitutionId);

        builder
            .HasOne<StudentEntity>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.StudentId);
    }
}
