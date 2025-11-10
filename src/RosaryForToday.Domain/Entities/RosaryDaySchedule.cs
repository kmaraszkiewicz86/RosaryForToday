namespace RosaryForToday.Domain.Entities;

public class RosaryDaySchedule
{
    public int Id { get; set; }
    public int RosaryTypeId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }

    // Foreign key for language (new)
    public int LanguageId { get; set; }

    // Navigation properties
    public RosaryType RosaryType { get; set; } = null!;
    public Language Language { get; set; } = null!;
}
