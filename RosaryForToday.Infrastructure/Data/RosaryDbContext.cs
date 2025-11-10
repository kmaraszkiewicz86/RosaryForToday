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
        
        // Language configuration
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Code).IsUnique();
        });
        
        // RosaryType configuration
        modelBuilder.Entity<RosaryType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            
            entity.HasOne(e => e.Language)
                .WithMany(l => l.RosaryTypes)
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // RosaryReflection configuration
        modelBuilder.Entity<RosaryReflection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Content).IsRequired();
            
            entity.HasOne(e => e.RosaryType)
                .WithMany(rt => rt.RosaryReflections)
                .HasForeignKey(e => e.RosaryTypeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(e => e.Language)
                .WithMany(l => l.RosaryReflections)
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        // RosaryDaySchedule configuration
        modelBuilder.Entity<RosaryDaySchedule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DayOfWeek).IsRequired();
            
            entity.HasOne(e => e.RosaryType)
                .WithMany(rt => rt.RosaryDaySchedules)
                .HasForeignKey(e => e.RosaryTypeId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasIndex(e => new { e.RosaryTypeId, e.DayOfWeek }).IsUnique();
        });
        
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
