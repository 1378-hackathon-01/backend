using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class UserInstitutionEntityConfiguration : BaseInstitutionModelEntityConfiguration<UserInstitutionEntity>, IEntityTypeConfiguration<UserInstitutionEntity>
{
    public new void Configure(EntityTypeBuilder<UserInstitutionEntity> builder)
    {
        base.Configure(builder);
    }
}
