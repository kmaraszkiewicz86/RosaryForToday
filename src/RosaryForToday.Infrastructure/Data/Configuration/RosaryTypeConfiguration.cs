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

        builder.HasOne(e => e.Language)
            .WithMany(l => l.RosaryTypes)
            .HasForeignKey(e => e.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<RosaryType> builder)
    {
        builder.HasData(
            new RosaryType { Id = SeedDataIds.RosaryTypes.JoyfulPolish, Name = "Tajemnice Radosne", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryType { Id = SeedDataIds.RosaryTypes.SorrowfulPolish, Name = "Tajemnice Bolesne", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryType { Id = SeedDataIds.RosaryTypes.LuminousPolish, Name = "Tajemnice Œwiat³a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryType { Id = SeedDataIds.RosaryTypes.GloriousPolish, Name = "Tajemnice Chwalebne", LanguageId = SeedDataIds.Languages.Polish }
        );
    }
}
