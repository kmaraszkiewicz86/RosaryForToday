using SimpleCqrs;

namespace RosaryForToday.Models.Queries;

public class GetRosaryForTodayQuery : IQuery<RosaryForTodayDto>
{
    public int LanguageId { get; set; }
    public DateTime Date { get; set; }
}

public class RosaryForTodayDto
{
    public int RosaryTypeId { get; set; }
    public string RosaryTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DayOfWeek DayOfWeek { get; set; }
    public string? ReflectionTitle { get; set; }
    public string? ReflectionContent { get; set; }
}