using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class InstitutionEntityConfiguration : BaseModelEntityConfiguration<InstitutionEntity>
{
    public new void Configure(EntityTypeBuilder<InstitutionEntity> builder)
    {
        base.Configure(builder);
    }
}