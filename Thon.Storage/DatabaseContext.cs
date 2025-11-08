using Microsoft.EntityFrameworkCore;
using Thon.Storage.Configurations;
using Thon.Storage.Entities;

namespace Thon.Storage;

internal class DatabaseContext : DbContext
{
    public DbSet<UserAdminEntity> AdminUsers => Set<UserAdminEntity>();

    public DbSet<UserAdminAuthEntity> AdminUserAuths => Set<UserAdminAuthEntity>();

    public DbSet<ApiTokenEntity> ApiTokens => Set<ApiTokenEntity>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<ApiTokenEntity>(new ApiTokenEntityConfiguration());

        modelBuilder.ApplyConfiguration<UserAdminEntity>(new UserAdminEntityConfiguration());
        modelBuilder.ApplyConfiguration<UserAdminAuthEntity>(new UserAdminAuthEntityConfiguration());
    }
}
