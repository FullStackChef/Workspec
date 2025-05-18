using Microsoft.EntityFrameworkCore;
using Workspec.DataAccess.Models.Personnel;
using Workspec.DataAccess.Models.Products;
using Workspec.DataAccess.Models.Projects;
using Workspec.DataAccess.Models.Systems;

namespace Workspec.DataAccess.EF
{
    public class WorkspecDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ProjectDbRecord> Projects { get; set; }
        public DbSet<UserRequirementDbRecord> UserRequirements { get; set; }
        public DbSet<RiskDbRecord> Risks { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StakeholderDbRecord>().ToTable(nameof(StakeholderDbRecord), "personnel");

            modelBuilder.Entity<ProductDbRecord>().ToTable(nameof(ProductDbRecord), "products");
            modelBuilder.Entity<ModuleDbRecord>().ToTable(nameof(ModuleDbRecord), "products");
            modelBuilder.Entity<FeatureDbRecord>().ToTable(nameof(FeatureDbRecord), "products");

            modelBuilder.Entity<SystemDbRecord>().ToTable(nameof(SystemDbRecord), "systems");
            modelBuilder.Entity<ComponentDbRecord>().ToTable(nameof(ComponentDbRecord), "systems");
            modelBuilder.Entity<CapabilityDbRecord>().ToTable(nameof(CapabilityDbRecord), "systems");

            modelBuilder.Entity<ProjectDbRecord>().ToTable(nameof(ProjectDbRecord), "projects");
            modelBuilder.Entity<UserRequirementDbRecord>().ToTable(nameof(UserRequirementDbRecord), "projects");
            modelBuilder.Entity<RiskDbRecord>().ToTable(nameof(RiskDbRecord), "projects");

            modelBuilder.Entity<ProjectDbRecord>()
                .HasMany(ProjectDbRecord => ProjectDbRecord.UserRequirements)
                .WithMany(UserRequirementDbRecord => UserRequirementDbRecord.Projects);

            modelBuilder.Entity<ComponentTypeDbRecord>().ToTable(nameof(ComponentTypeDbRecord), "systems");
            modelBuilder.Entity<ComponentTypeAttributeDbRecord>().ToTable(nameof(ComponentTypeAttributeDbRecord), "systems");

            modelBuilder.Entity<ComponentTypeDbRecord>()
                .HasMany(ComponentTypeDbRecord => ComponentTypeDbRecord.Attributes)
                .WithOne().HasForeignKey(componentTypeDbRecord=> componentTypeDbRecord.ComponentTypeId);

            modelBuilder.Entity<ComponentTypeDbRecord>()
                .Navigation(ComponentTypeDbRecord => ComponentTypeDbRecord.Attributes)
                .AutoInclude();
        }
    }
}
