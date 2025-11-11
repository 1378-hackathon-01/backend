using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class FacultyEntityConfiguration : BaseInstitutionModelEntityConfiguration<FacultyEntity>
{
    public new void Configure(EntityTypeBuilder<FacultyEntity> builder)
    {
        base.Configure(builder);
    }
}
