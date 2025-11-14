using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentRequestInstitutionFacultyEntityConfiguration : BaseModelEntityConfiguration<StudentRequestInstitutionFacultyEntity>, IEntityTypeConfiguration<StudentRequestInstitutionFacultyEntity>
{
    public new void Configure(EntityTypeBuilder<StudentRequestInstitutionFacultyEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<StudentRequestInstitutionEntity>()
            .WithMany()
            .HasForeignKey(x => x.StudentRequestInstitutionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.StudentRequestInstitutionId);

        builder
            .HasOne<FacultyEntity>()
            .WithMany()
            .HasForeignKey(x => x.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.FacultyId);
    }
}