using Microsoft.EntityFrameworkCore;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RosaryForToday.Infrastructure.DbQueries;

public class RosaryDbQuery : IRosaryDbQuery
{
    private readonly RosaryDbContext _db;

    public RosaryDbQuery(RosaryDbContext db)
    {
        _db = db;
    }

    public async Task<RosaryForTodayDto?> GetRosaryForDateAsync(LanguageTypeEnum language, System.DateTime date, CancellationToken ct = default)
    {
        var targetDay = date.DayOfWeek;

        var schedule = await _db.RosaryDaySchedules
            .Include(s => s.RosaryType)
            .Where(s => s.DayOfWeek == targetDay && s.LanguageId == (int)language)
            .Select(s => new { s.RosaryTypeId, s.RosaryType })
            .FirstOrDefaultAsync(ct);

        if (schedule == null) return null;

        var reflectionEntities = await _db.RosaryReflections
            .Where(r => r.RosaryTypeId == schedule.RosaryTypeId && r.LanguageId == (int)language)
            .ToListAsync(ct);

        RosaryForTodayDto result = new()
        {
            RosaryTypeId = schedule.RosaryType.Id,
            RosaryTypeName = schedule.RosaryType.Name,
            Description = string.Empty,
            DayOfWeek = targetDay,
            RosaryReflections = [.. reflectionEntities.Select(r => new RosaryReflectionDto
            {
                Id = r.Id,
                RosaryTypeId = r.RosaryTypeId,
                Title = r.Title,
                Content = r.Content
            })]
        };

        return result;
    }
}
