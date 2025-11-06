using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class UserAdminAuthEntityConfiguration : BaseModelEntityConfiguration<UserAdminAuthEntity>
{
    public new void Configure(EntityTypeBuilder<UserAdminAuthEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<UserAdminEntity>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasIndex(x => x.UserId);
    }
}