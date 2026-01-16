using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using SimpleCqrs;

namespace RosaryForToday.Models.Queries;

public class GetRosaryTitleForTodayQuery : IQuery<RosaryTitleDto>
{
    public LanguageTypeEnum Language { get; set; }
}
