using RosaryForToday.Models.Enums;
using SimpleCqrs;

namespace RosaryForToday.Models.Queries;

public class GetRosaryTitleForTodayQuery : IQuery<string>
{
    public LanguageTypeEnum Language { get; set; }
}
