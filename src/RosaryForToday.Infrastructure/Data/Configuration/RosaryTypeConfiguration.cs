using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RosaryForToday.Domain.Entities;

namespace RosaryForToday.Infrastructure.Data.Configuration;

public class RosaryTypeConfiguration : IEntityTypeConfiguration<RosaryType>
{
    public void Configure(EntityTypeBuilder<RosaryType> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(1000);

        builder.HasOne(e => e.Language)
        .WithMany(l => l.RosaryTypes)
        .HasForeignKey(e => e.LanguageId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
