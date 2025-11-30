using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Queries;
using SimpleCqrs;

namespace RosaryForToday.ApplicationLayer.QueryHandlers;

public class GetRosaryForTodayQueryHandler(IRosaryDbQuery _dbQuery) : IAsyncQueryHandler<GetRosaryForTodayQuery, RosaryDto?>
{
    public async Task<RosaryDto?> HandleAsync(GetRosaryForTodayQuery query, CancellationToken cancellationToken = default)
        => await _dbQuery.GetRosaryForDateAsync(query.Language, cancellationToken);
}