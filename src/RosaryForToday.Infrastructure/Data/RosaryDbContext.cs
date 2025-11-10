using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RosaryForToday.Domain.Entities;

namespace RosaryForToday.Infrastructure.Data;

public class RosaryDbContext : DbContext
{
    public RosaryDbContext(DbContextOptions<RosaryDbContext> options) : base(options)
    {
    }

    public DbSet<Language> Languages { get; set; }
    public DbSet<RosaryType> RosaryTypes { get; set; }
    public DbSet<RosaryReflection> RosaryReflections { get; set; }
    public DbSet<RosaryDaySchedule> RosaryDaySchedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations from the assembly (types in RosaryForToday.Infrastructure.Data.Configuration)
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t =>
            t.Namespace != null && t.Namespace.EndsWith(".Configuration"));

        // Seed initial data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Languages
        modelBuilder.Entity<Language>().HasData(
            new Language { Id = 1, Code = "en", Name = "English" },
            new Language { Id = 2, Code = "pl", Name = "Polish" }
        );
    }
}
