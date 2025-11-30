using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Queries;
using SimpleCqrs;

namespace RosaryForToday.ApplicationLayer.QueryHandlers;

public class GetAllRosariesQueryHandler(IRosaryDbQuery _dbQuery) : IAsyncQueryHandler<GetAllRosariesQuery, IEnumerable<RosaryDto>>
{
    public Task<IEnumerable<RosaryDto>> HandleAsync(GetAllRosariesQuery command, CancellationToken cancellationToken = default)
        => _dbQuery.GetAllRosariesExceptTodayAsync(command.Language, cancellationToken);
}