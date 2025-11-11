using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentInstitutionEntityConfiguration : BaseModelEntityConfiguration<StudentInstitutionEntity>
{
    public new void Configure(EntityTypeBuilder<StudentInstitutionEntity> builder)
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
