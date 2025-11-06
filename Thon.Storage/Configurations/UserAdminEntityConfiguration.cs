using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thon.Storage.Entities;

namespace Thon.Storage.Configurations;

internal class UserAdminEntityConfiguration : BaseModelEntityConfiguration<UserAdminEntity>
{
    public new void Configure(EntityTypeBuilder<UserAdminEntity> builder)
    {
        base.Configure(builder);

        builder
            .HasIndex(x => x.Login)
            .IsUnique();

        builder
            .HasIndex(x => new { x.Login, x.PasswordHash });
    }
}
