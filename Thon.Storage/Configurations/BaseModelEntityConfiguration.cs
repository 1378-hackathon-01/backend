using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class BaseModelEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModelEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => x.CreatedAtUtc)
            .IsDescending();

        builder
            .HasIndex(x => x.UpdatedAtUtc)
            .IsDescending();
    }
}
