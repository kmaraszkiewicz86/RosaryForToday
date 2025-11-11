namespace RosaryForToday.Models.Dtos;

public class RosaryForTodayDto
{
    public int RosaryTypeId { get; set; }
    public string RosaryTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DayOfWeek DayOfWeek { get; set; }
    public RosaryReflectionDto[] RosaryReflections { get; set; } = [];
}
