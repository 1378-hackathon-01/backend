using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class SubjectEntityConfiguration : BaseInstitutionModelEntityConfiguration<SubjectEntity>, IEntityTypeConfiguration<SubjectEntity>
{
    public new void Configure(EntityTypeBuilder<SubjectEntity> builder)
    {
        base.Configure(builder);
    }
}
