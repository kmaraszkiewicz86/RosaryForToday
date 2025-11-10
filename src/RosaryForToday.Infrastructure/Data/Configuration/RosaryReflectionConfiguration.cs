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

        // Seed RosaryReflections (5 per rosary type, Polish, Content = "n/a")
        builder.HasData(
            // Radosne
            new RosaryReflection { Id = 1, RosaryTypeId = SeedDataIds.RosaryTypes.Joyful, Title = "Zwiastowanie Pañskie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 2, RosaryTypeId = SeedDataIds.RosaryTypes.Joyful, Title = "Nawiedzenie œw. El¿biety", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 3, RosaryTypeId = SeedDataIds.RosaryTypes.Joyful, Title = "Narodzenie Pana Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 4, RosaryTypeId = SeedDataIds.RosaryTypes.Joyful, Title = "Ofiarowanie w œwi¹tyni", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 5, RosaryTypeId = SeedDataIds.RosaryTypes.Joyful, Title = "Odnalezienie Jezusa w œwi¹tyni", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },

            // Bolesne
            new RosaryReflection { Id = 6, RosaryTypeId = SeedDataIds.RosaryTypes.Sorrowful, Title = "Modlitwa w Ogrójcu", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 7, RosaryTypeId = SeedDataIds.RosaryTypes.Sorrowful, Title = "Biczowanie Pana Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 8, RosaryTypeId = SeedDataIds.RosaryTypes.Sorrowful, Title = "Cierniem Ukoronowanie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 9, RosaryTypeId = SeedDataIds.RosaryTypes.Sorrowful, Title = "Droga Krzy¿owa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 10, RosaryTypeId = SeedDataIds.RosaryTypes.Sorrowful, Title = "Ukrzy¿owanie i œmieræ Pana Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },

            // Œwiat³a
            new RosaryReflection { Id = 11, RosaryTypeId = SeedDataIds.RosaryTypes.Luminous, Title = "Chrzest Jezusa w Jordanie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 12, RosaryTypeId = SeedDataIds.RosaryTypes.Luminous, Title = "Objawienie siê Jezusa w Kanie Galilejskiej", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 13, RosaryTypeId = SeedDataIds.RosaryTypes.Luminous, Title = "G³oszenie Królestwa Bo¿ego", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 14, RosaryTypeId = SeedDataIds.RosaryTypes.Luminous, Title = "Przemienienie Pañskie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 15, RosaryTypeId = SeedDataIds.RosaryTypes.Luminous, Title = "Ustanowienie Eucharystii", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },

            // Chwalebne
            new RosaryReflection { Id = 16, RosaryTypeId = SeedDataIds.RosaryTypes.Glorious, Title = "Zmartwychwstanie Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 17, RosaryTypeId = SeedDataIds.RosaryTypes.Glorious, Title = "Wniebowst¹pienie Pana", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 18, RosaryTypeId = SeedDataIds.RosaryTypes.Glorious, Title = "Zes³anie Ducha Œwiêtego", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 19, RosaryTypeId = SeedDataIds.RosaryTypes.Glorious, Title = "Wniebowziêcie Najœwiêtszej Maryi Panny", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 20, RosaryTypeId = SeedDataIds.RosaryTypes.Glorious, Title = "Ukoronowanie Maryi na Królow¹ Nieba i Ziemi", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish }
        );
    }
}