using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RosaryForToday.Domain.Entities;
using RosaryForToday.Infrastructure.Data.Configuration;

namespace RosaryForToday.Infrastructure.Data.Configuration;

public class RosaryDayScheduleConfiguration : IEntityTypeConfiguration<RosaryDaySchedule>
{
    public void Configure(EntityTypeBuilder<RosaryDaySchedule> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.DayOfWeek).IsRequired();

        // new LanguageId property
        builder.Property(e => e.LanguageId).IsRequired();

        builder.HasOne(e => e.RosaryType)
            .WithMany(rt => rt.RosaryDaySchedules)
            .HasForeignKey(e => e.RosaryTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // relation to Language
        builder.HasOne(e => e.Language)
            .WithMany()
            .HasForeignKey(e => e.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.RosaryTypeId, e.DayOfWeek }).IsUnique();

        // Seed schedules — entries for Polish and English counterparts
        Seed(builder);
    }

    private void Seed(EntityTypeBuilder<RosaryDaySchedule> builder)
    {
        builder.HasData(
            // Monday - Joyful
            new RosaryDaySchedule { Id = 1, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, DayOfWeek = DayOfWeek.Monday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 2, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, DayOfWeek = DayOfWeek.Monday, LanguageId = SeedDataIds.Languages.English },

            // Tuesday - Sorrowful
            new RosaryDaySchedule { Id = 3, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, DayOfWeek = DayOfWeek.Tuesday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 4, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, DayOfWeek = DayOfWeek.Tuesday, LanguageId = SeedDataIds.Languages.English },

            // Wednesday - Glorious
            new RosaryDaySchedule { Id = 5, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, DayOfWeek = DayOfWeek.Wednesday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 6, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, DayOfWeek = DayOfWeek.Wednesday, LanguageId = SeedDataIds.Languages.English },

            // Thursday - Luminous
            new RosaryDaySchedule { Id = 7, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousPolish, DayOfWeek = DayOfWeek.Thursday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 8, RosaryTypeId = SeedDataIds.RosaryTypes.LuminousEnglish, DayOfWeek = DayOfWeek.Thursday, LanguageId = SeedDataIds.Languages.English },

            // Friday - Sorrowful
            new RosaryDaySchedule { Id = 9, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulPolish, DayOfWeek = DayOfWeek.Friday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 10, RosaryTypeId = SeedDataIds.RosaryTypes.SorrowfulEnglish, DayOfWeek = DayOfWeek.Friday, LanguageId = SeedDataIds.Languages.English },

            // Saturday - Joyful
            new RosaryDaySchedule { Id = 11, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulPolish, DayOfWeek = DayOfWeek.Saturday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 12, RosaryTypeId = SeedDataIds.RosaryTypes.JoyfulEnglish, DayOfWeek = DayOfWeek.Saturday, LanguageId = SeedDataIds.Languages.English },

            // Sunday - Glorious
            new RosaryDaySchedule { Id = 13, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousPolish, DayOfWeek = DayOfWeek.Sunday, LanguageId = SeedDataIds.Languages.Polish },
            new RosaryDaySchedule { Id = 14, RosaryTypeId = SeedDataIds.RosaryTypes.GloriousEnglish, DayOfWeek = DayOfWeek.Sunday, LanguageId = SeedDataIds.Languages.English }
        );
    }
}
