using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using SimpleCqrs;

namespace RosaryForToday.Models.Queries;

public class GetAllRosariesQuery : IQuery<IEnumerable<RosaryForTodayDto>>
{
    public LanguageTypeEnum Language { get; set; }
}