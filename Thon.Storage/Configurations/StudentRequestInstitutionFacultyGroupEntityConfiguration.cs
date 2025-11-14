using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentRequestInstitutionFacultyGroupEntityConfiguration 
    : BaseModelEntityConfiguration<StudentRequestInstitutionFacultyGroupEntity>, IEntityTypeConfiguration<StudentRequestInstitutionFacultyGroupEntity>
{
    public new void Configure(EntityTypeBuilder<StudentRequestInstitutionFacultyGroupEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<StudentRequestInstitutionFacultyEntity>()
            .WithMany()
            .HasForeignKey(x => x.StudentRequestInstitutionFacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.StudentRequestInstitutionFacultyId);

        builder
            .HasOne<GroupEntity>()
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.GroupId);
    }
}
