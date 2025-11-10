using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RosaryForToday.Domain.Entities;

namespace RosaryForToday.Infrastructure.Data.Configuration;

public class RosaryReflectionConfiguration : IEntityTypeConfiguration<RosaryReflection>
{
    public void Configure(EntityTypeBuilder<RosaryReflection> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Content).IsRequired();

        builder.HasOne(e => e.RosaryType)
        .WithMany(rt => rt.RosaryReflections)
        .HasForeignKey(e => e.RosaryTypeId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Language)
        .WithMany(l => l.RosaryReflections)
        .HasForeignKey(e => e.LanguageId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
