using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Asset> Assets { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectAsset> ProjectAssets { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAsset> UserAssets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>().HasQueryFilter(a=> !a.IsDeleted);
        modelBuilder.Entity<Education>().HasQueryFilter(e=> !e.IsDeleted);
        modelBuilder.Entity<Experience>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Project>().HasQueryFilter(p=> !p.IsDeleted);
        modelBuilder.Entity<ProjectAsset>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Skill>().HasQueryFilter(s=> !s.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u=> !u.IsDeleted);
        modelBuilder.Entity<UserAsset>().HasQueryFilter(u=> !u.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}
