using Microsoft.EntityFrameworkCore;
using Thon.Storage.Configurations;
using Thon.Storage.Entities;

namespace Thon.Storage;

internal class DatabaseContext : DbContext
{
    public DbSet<UserAdminEntity> AdminUsers => Set<UserAdminEntity>();

    public DbSet<UserAdminAuthEntity> AdminUserAuths => Set<UserAdminAuthEntity>();

    public DbSet<ApiTokenEntity> ApiTokens => Set<ApiTokenEntity>();

    public DbSet<InstitutionEntity> Institutions => Set<InstitutionEntity>();

    public DbSet<UserInstitutionEntity> InstitutionUsers => Set<UserInstitutionEntity>();

    public DbSet<UserInstitutionAuthEntity> InstitutionUserAuths => Set<UserInstitutionAuthEntity>();

    public DbSet<FacultyEntity> Faculties => Set<FacultyEntity>();

    public DbSet<GroupEntity> Groups => Set<GroupEntity>();

    public DbSet<StudentEntity> Students => Set<StudentEntity>();

    public DbSet<StudentApprovedEntity> StudentsApproved => Set<StudentApprovedEntity>();

    public DbSet<StudentRequestInstitutionEntity> StudentRequestInstitutions => Set<StudentRequestInstitutionEntity>();

    public DbSet<StudentRequestInstitutionFacultyEntity> StudentRequestInstitutionsFaculties => Set<StudentRequestInstitutionFacultyEntity>();

    public DbSet<StudentRequestInstitutionFacultyGroupEntity> StudentRequestInstitutionFacultyGroups => Set<StudentRequestInstitutionFacultyGroupEntity>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<ApiTokenEntity>(new ApiTokenEntityConfiguration());

        modelBuilder.ApplyConfiguration<UserAdminEntity>(new UserAdminEntityConfiguration());
        modelBuilder.ApplyConfiguration<UserAdminAuthEntity>(new UserAdminAuthEntityConfiguration());

        modelBuilder.ApplyConfiguration<InstitutionEntity>(new InstitutionEntityConfiguration());
        modelBuilder.ApplyConfiguration<UserInstitutionEntity>(new UserInstitutionEntityConfiguration());
        modelBuilder.ApplyConfiguration<UserInstitutionAuthEntity>(new UserInstitutionAuthEntityConfiguration());
        modelBuilder.ApplyConfiguration<FacultyEntity>(new FacultyEntityConfiguration());
        modelBuilder.ApplyConfiguration<GroupEntity>(new GroupEntityConfiguration());

        modelBuilder.ApplyConfiguration<StudentEntity>(new StudentEntityConfiguration());
        modelBuilder.ApplyConfiguration<StudentRequestInstitutionEntity>(new StudentRequestInstitutionEntityConfiguration());
        modelBuilder.ApplyConfiguration<StudentRequestInstitutionFacultyEntity>(new StudentRequestInstitutionFacultyEntityConfiguration());
        modelBuilder.ApplyConfiguration<StudentRequestInstitutionFacultyGroupEntity>(new StudentRequestInstitutionFacultyGroupEntityConfiguration());
        modelBuilder.ApplyConfiguration<StudentApprovedEntity>(new StudentApprovedEntityConfiguration());
    }
}
