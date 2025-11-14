using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class BaseInstitutionModelEntityConfiguration<T> : BaseModelEntityConfiguration<T>, IEntityTypeConfiguration<T> where T : BaseInstitutionModelEntity
{
    public new void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<InstitutionEntity>()
            .WithMany()
            .HasForeignKey(x => x.InstitutionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.InstitutionId);
    }
}