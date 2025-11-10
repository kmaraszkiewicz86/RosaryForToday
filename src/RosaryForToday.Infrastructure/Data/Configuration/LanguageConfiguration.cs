using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RosaryForToday.Domain.Entities;

namespace RosaryForToday.Infrastructure.Data.Configuration;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(10);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => e.Code).IsUnique();

        // Seed languages
        builder.HasData(
            new Language { Id = SeedDataIds.Languages.English, Code = "EN", Name = "English" },
            new Language { Id = SeedDataIds.Languages.Polish, Code = "PL", Name = "Polski" }
        );
    }
}
