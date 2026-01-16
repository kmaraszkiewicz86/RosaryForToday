using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Queries;
using SimpleCqrs;

namespace RosaryForToday.ApplicationLayer.QueryHandlers;

public class GetRosaryTitleForTodayQueryHandler(IRosaryDbQuery _dbQuery) : IQueryHandler<GetRosaryTitleForTodayQuery, RosaryTitleDto>
{
    public RosaryTitleDto Handle(GetRosaryTitleForTodayQuery query)
        => _dbQuery.GetRosaryTitleForToday(query.Language);
}
