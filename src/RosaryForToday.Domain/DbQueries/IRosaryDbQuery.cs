using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;

namespace RosaryForToday.Domain.DbQueries;

public interface IRosaryDbQuery
{
    Task<RosaryForTodayDto?> GetRosaryForDateAsync(LanguageTypeEnum language, DateTime date, CancellationToken ct = default);
}