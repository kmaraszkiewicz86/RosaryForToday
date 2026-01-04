using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Queries;
using SimpleCqrs;

namespace RosaryForToday.ApplicationLayer.QueryHandlers;

public class GetRosaryTitleForTodayQueryHandler(IRosaryDbQuery _dbQuery) : IQueryHandler<GetRosaryTitleForTodayQuery, string>
{
    public string Handle(GetRosaryTitleForTodayQuery query)
        => _dbQuery.GetRosaryTitleForToday(query.Language);
}
