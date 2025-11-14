using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class GroupEntityConfiguration : BaseModelEntityConfiguration<GroupEntity>, IEntityTypeConfiguration<GroupEntity>
{
    public new void Configure(EntityTypeBuilder<GroupEntity> builder)
    {
        base.Configure(builder);

        builder
             .HasOne<FacultyEntity>()
             .WithMany()
             .HasForeignKey(x => x.FacultyId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
