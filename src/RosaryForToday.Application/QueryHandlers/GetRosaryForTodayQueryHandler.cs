using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Queries;

namespace RosaryForToday.Application.QueryHandlers;

public class GetRosaryForTodayQueryHandler
{
    private readonly IRosaryDbQuery _dbQuery;

    public GetRosaryForTodayQueryHandler(IRosaryDbQuery dbQuery)
    {
        _dbQuery = dbQuery;
    }

    public async Task<RosaryForTodayDto?> Handle(GetRosaryForTodayQuery query, CancellationToken ct = default)
        => await _dbQuery.GetRosaryForDateAsync(query.Language, query.Date, ct);
}
