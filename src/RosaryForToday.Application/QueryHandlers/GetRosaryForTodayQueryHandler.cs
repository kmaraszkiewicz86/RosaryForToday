using Microsoft.EntityFrameworkCore;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Models.Queries;

namespace RosaryForToday.Application.QueryHandlers;

public class GetRosaryForTodayQueryHandler
{
    private readonly RosaryDbContext _db;

    public GetRosaryForTodayQueryHandler(RosaryDbContext db)
    {
        _db = db;
    }

    public async Task<RosaryForTodayDto?> Handle(GetRosaryForTodayQuery query, CancellationToken ct = default)
    {
        var targetDay = query.Date.DayOfWeek;

        var schedule = await _db.RosaryDaySchedules
            .Include(s => s.RosaryType)
            .Where(s => s.DayOfWeek == targetDay)
            .Select(s => new { s.Id, s.RosaryTypeId, s.RosaryType })
            .FirstOrDefaultAsync(ct);

        if (schedule == null)
            return null;

        var reflection = await _db.RosaryReflections
            .Where(r => r.RosaryTypeId == schedule.RosaryTypeId && r.LanguageId == query.LanguageId)
            .FirstOrDefaultAsync(ct);

        return new RosaryForTodayDto
        {
            RosaryTypeId = schedule.RosaryType.Id,
            RosaryTypeName = schedule.RosaryType.Name,
            Description = schedule.RosaryType.Description,
            DayOfWeek = targetDay,
            ReflectionTitle = reflection?.Title,
            ReflectionContent = reflection?.Content
        };
    }
}