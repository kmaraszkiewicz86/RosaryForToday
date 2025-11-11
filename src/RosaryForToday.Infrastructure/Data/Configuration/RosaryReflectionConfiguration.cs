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
            new RosaryReflection { Id = 1, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, Title = "Zwiastowanie Pañskie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 2, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, Title = "Nawiedzenie œw. El¿biety", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 3, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, Title = "Narodzenie Pana Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 4, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, Title = "Ofiarowanie w œwi¹tyni", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 5, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, Title = "Odnalezienie Jezusa w œwi¹tyni", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },

            // Bolesne
            new RosaryReflection { Id = 6, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, Title = "Modlitwa w Ogrójcu", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 7, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, Title = "Biczowanie Pana Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 8, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, Title = "Cierniem Ukoronowanie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 9, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, Title = "Droga Krzy¿owa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 10, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, Title = "Ukrzy¿owanie i œmieræ Pana Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },

            // Œwiat³a
            new RosaryReflection { Id = 11, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousPolish, Title = "Chrzest Jezusa w Jordanie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 12, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousPolish, Title = "Objawienie siê Jezusa w Kanie Galilejskiej", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 13, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousPolish, Title = "G³oszenie Królestwa Bo¿ego", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 14, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousPolish, Title = "Przemienienie Pañskie", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 15, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousPolish, Title = "Ustanowienie Eucharystii", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },

            // Chwalebne
            new RosaryReflection { Id = 16, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, Title = "Zmartwychwstanie Jezusa", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 17, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, Title = "Wniebowst¹pienie Pana", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 18, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, Title = "Zes³anie Ducha Œwiêtego", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 19, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, Title = "Wniebowziêcie Najœwiêtszej Maryi Panny", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },
            new RosaryReflection { Id = 20, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, Title = "Ukoronowanie Maryi na Królow¹ Nieba i Ziemi", Content = "n/a", LanguageId = SeedDataIds.Languages.Polish },


            // English
            new RosaryReflection { Id = 21, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, Title = "The Annunciation", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 22, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, Title = "The Visitation", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 23, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, Title = "The Nativity (Birth of Our Lord)", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 24, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, Title = "The Presentation", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 25, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, Title = "The Finding in the Temple", Content = "n/a", LanguageId = SeedDataIds.Languages.English },

            new RosaryReflection { Id = 26, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, Title = "The Agony in the Garden", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 27, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, Title = "The Scourging at the Pillar", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 28, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, Title = "The Crowning with Thorns", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 29, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, Title = "The Carrying of the Cross", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 30, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, Title = "The Crucifixion and Death of Our Lord", Content = "n/a", LanguageId = SeedDataIds.Languages.English },

            new RosaryReflection { Id = 31, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousEnglish, Title = "The Baptism of Jesus in the Jordan", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 32, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousEnglish, Title = "The Wedding at Cana", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 33, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousEnglish, Title = "The Proclamation of the Kingdom of God", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 34, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousEnglish, Title = "The Transfiguration", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 35, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousEnglish, Title = "The Institution of the Eucharist", Content = "n/a", LanguageId = SeedDataIds.Languages.English },

            new RosaryReflection { Id = 36, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, Title = "The Resurrection", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 37, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, Title = "The Ascension", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 38, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, Title = "The Descent of the Holy Spirit (Pentecost)", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 39, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, Title = "The Assumption of the Blessed Virgin Mary", Content = "n/a", LanguageId = SeedDataIds.Languages.English },
            new RosaryReflection { Id = 40, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, Title = "The Coronation of the Blessed Virgin Mary as Queen of Heaven and Earth", Content = "n/a", LanguageId = SeedDataIds.Languages.English }
        );
    }
}