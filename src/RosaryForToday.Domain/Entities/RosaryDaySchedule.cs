namespace RosaryForToday.Domain.Entities;

public class RosaryDaySchedule
{
    public int Id { get; set; }
    public int RosaryTypeId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    
    // Navigation properties
    public RosaryType RosaryType { get; set; } = null!;
}
