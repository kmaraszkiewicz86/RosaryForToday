namespace RosaryForToday.Domain.Entities;

public class RosaryType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LanguageId { get; set; }
    
    // Navigation properties
    public Language Language { get; set; } = null!;
    public ICollection<RosaryReflection> RosaryReflections { get; set; } = new List<RosaryReflection>();
    public ICollection<RosaryDaySchedule> RosaryDaySchedules { get; set; } = new List<RosaryDaySchedule>();
}
