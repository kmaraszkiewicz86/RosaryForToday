using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;

namespace RosaryForToday.Domain.DbQueries;

public interface IRosaryDbQuery
{
    Task<RosaryDto?> GetRosaryForDateAsync(LanguageTypeEnum language, CancellationToken ct = default);

    Task<IEnumerable<RosaryDto>> GetAllRosariesExceptTodayAsync(LanguageTypeEnum language, CancellationToken ct = default);
}