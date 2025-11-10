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

        builder.HasOne(e => e.RosaryType)
        .WithMany(rt => rt.RosaryDaySchedules)
        .HasForeignKey(e => e.RosaryTypeId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.RosaryTypeId, e.DayOfWeek }).IsUnique();
    }
}
