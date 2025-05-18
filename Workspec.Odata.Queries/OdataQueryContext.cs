using Microsoft.EntityFrameworkCore;
using Workspec.Contracts.Queries;
using Workspec.DataAccess.Models.Projects;
using Workspec.DataAccess.Models.Products;

namespace Workspec.Odata.Queries;

public class OdataQueryContext(DbContextOptions<OdataQueryContext> options) : DbContext(options)
{
    public DbSet<ProjectQueryModel> Projects { get; set; }
    public DbSet<ProductQueryModel> Products { get; set; }
    public DbSet<ModuleQueryModel> Modules { get; set; }
    public DbSet<FeatureQueryModel> Features { get; set; }
    public DbSet<UserRequirementQueryModel> UserRequirements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectQueryModel>().ToTable(nameof(ProjectDbRecord), "projects");
        modelBuilder.Entity<ProjectQueryModel>()
          .HasOne(project => project.Product)
          .WithMany(product => product.Projects)
          .HasForeignKey(project => project.ProductId);
        modelBuilder.Entity<ProjectQueryModel>().Navigation(project => project.Product).AutoInclude();

        modelBuilder.Entity<ProjectDbRecord>()
            .HasIndex(ProjectDbRecord => ProjectDbRecord.Path)
            .IsUnique();


        modelBuilder.Entity<ProductQueryModel>().ToTable(nameof(ProductDbRecord), "products");
        modelBuilder.Entity<ProductQueryModel>()
            .HasMany(product=>product.Modules)
            .WithOne(module=>module.Product)
            .HasForeignKey(module => module.ProductId);
        modelBuilder.Entity<ProductQueryModel>().Navigation(project => project.Modules).AutoInclude();


        modelBuilder.Entity<ModuleQueryModel>().ToTable(nameof(ModuleDbRecord), "products");
        modelBuilder.Entity<ModuleQueryModel>()
            .HasMany(product => product.Features)
            .WithOne(module => module.Module)
            .HasForeignKey(module => module.ModuleId);
        modelBuilder.Entity<ModuleQueryModel>().Navigation(project => project.Features).AutoInclude();


        modelBuilder.Entity<FeatureQueryModel>().ToTable(nameof(FeatureDbRecord), "products");
        modelBuilder.Entity<FeatureQueryModel>()
            .HasMany(product => product.UserRequirements)
            .WithOne(module => module.Feature)
            .HasForeignKey(module => module.FeatureId);
        modelBuilder.Entity<FeatureQueryModel>().Navigation(project => project.UserRequirements).AutoInclude();

        
        modelBuilder.Entity<UserRequirementQueryModel>().ToTable(nameof(UserRequirementDbRecord), "projects");

        modelBuilder.Entity<ProjectQueryModel>()
            .HasMany(ProjectDbRecord => ProjectDbRecord.UserRequirements)
            .WithMany(UserRequirementDbRecord => UserRequirementDbRecord.Projects);

        base.OnModelCreating(modelBuilder);
    }
}
