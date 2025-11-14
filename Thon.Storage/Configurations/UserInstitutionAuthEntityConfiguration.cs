using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class UserInstitutionAuthEntityConfiguration : BaseModelEntityConfiguration<UserInstitutionAuthEntity>, IEntityTypeConfiguration<UserInstitutionAuthEntity>
{
    public new void Configure(EntityTypeBuilder<UserInstitutionAuthEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<UserInstitutionEntity>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.UserId);
    }
}