using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Queries;

namespace RosaryForToday.ApplicationLayer.QueryHandlers;

public class GetRosaryForTodayQueryHandler(IRosaryDbQuery _dbQuery)
{
    public async Task<RosaryForTodayDto?> Handle(GetRosaryForTodayQuery query, CancellationToken ct = default)
        => await _dbQuery.GetRosaryForDateAsync(query.Language, query.Date, ct);
}