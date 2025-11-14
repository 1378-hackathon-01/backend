using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class StudentApprovedEntityConfiguration
    : BaseModelEntityConfiguration<StudentApprovedEntity>, IEntityTypeConfiguration<StudentApprovedEntity>
{
    public new void Configure(EntityTypeBuilder<StudentApprovedEntity> builder)
    {
        base.Configure(builder);

        builder
           .HasOne<StudentEntity>()
           .WithMany()
           .HasForeignKey(x => x.StudentId)
           .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.StudentId);

        builder
           .HasOne<InstitutionEntity>()
           .WithMany()
           .HasForeignKey(x => x.InstitutionId)
           .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.InstitutionId);

        builder
            .HasOne<FacultyEntity>()
            .WithMany()
            .HasForeignKey(x => x.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.FacultyId);

        builder
            .HasOne<GroupEntity>()
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.GroupId);
    }
}
