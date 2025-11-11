using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using SimpleCqrs;

namespace RosaryForToday.Models.Queries;

public class GetRosaryForTodayQuery : IQuery<RosaryForTodayDto>
{
    public LanguageTypeEnum Language { get; set; }
    public DateTime Date { get; set; }
}