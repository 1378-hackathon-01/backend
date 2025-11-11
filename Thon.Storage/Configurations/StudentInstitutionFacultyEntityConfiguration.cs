using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentInstitutionFacultyEntityConfiguration : BaseModelEntityConfiguration<StudentInstitutionFacultyEntity>
{
    public new void Configure(EntityTypeBuilder<StudentInstitutionFacultyEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<StudentInstitutionEntity>()
            .WithMany()
            .HasForeignKey(x => x.StudentInstitutionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.StudentInstitutionId);

        builder
            .HasOne<FacultyEntity>()
            .WithMany()
            .HasForeignKey(x => x.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.FacultyId);
    }
}