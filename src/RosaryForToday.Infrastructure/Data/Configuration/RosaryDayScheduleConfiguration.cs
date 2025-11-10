using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RosaryForToday.Domain.Entities;

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

        // relation to Language (no collection on Language specified -> use WithMany())
        builder.HasOne(e => e.Language)
            .WithMany()
            .HasForeignKey(e => e.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.RosaryTypeId, e.DayOfWeek }).IsUnique();
    }
}
