using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Queries;

namespace RosaryForToday.ApplicationLayer.QueryHandlers;

public class GetAllRosariesQueryHandler(IRosaryDbQuery _dbQuery)
{
    public async Task<IEnumerable<RosaryDto>> Handle(GetAllRosariesQuery query, CancellationToken ct = default)
        => await _dbQuery.GetAllRosariesExceptDateAsync(query.Language, ct);
}