using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class GroupSubjectEntityConfiguration : BaseModelEntityConfiguration<GroupSubjectEntity>, IEntityTypeConfiguration<GroupSubjectEntity>
{
    public new void Configure(EntityTypeBuilder<GroupSubjectEntity> builder)
    {
        base.Configure(builder);

        builder
             .HasOne<GroupEntity>()
             .WithMany()
             .HasForeignKey(x => x.GroupId)
             .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.GroupId);

        builder
             .HasOne<SubjectEntity>()
             .WithMany()
             .HasForeignKey(x => x.SubjectId)
             .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.SubjectId);
    }
}