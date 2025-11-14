using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class ApiTokenEntityConfiguration : BaseModelEntityConfiguration<ApiTokenEntity>, IEntityTypeConfiguration<ApiTokenEntity>
{
    public new void Configure(EntityTypeBuilder<ApiTokenEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(x => x.TokenHash)
            .IsUnique();
    }
}