using Microsoft.EntityFrameworkCore;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Infrastructure.Data;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;

namespace RosaryForToday.Infrastructure.DbQueries;

public class RosaryDbQuery : IRosaryDbQuery
{
    private readonly RosaryDbContext _db;

    public RosaryDbQuery(RosaryDbContext db)
    {
        _db = db;
    }

    public async Task<RosaryDto?> GetRosaryForDateAsync(LanguageTypeEnum language, CancellationToken ct = default)
    {
        DateTime date = DateTime.UtcNow;
        string dayNameText = GetPolishDayName(date.DayOfWeek);

        var schedule = await _db.RosaryDaySchedules
            .Include(s => s.RosaryType)
            .Where(s => s.DayOfWeek == date.DayOfWeek && s.LanguageId == (int)language)
            .Select(s => new { s.RosaryTypeId, s.RosaryType })
            .FirstOrDefaultAsync(ct);

        if (schedule == null) return null;

        var reflectionEntities = await _db.RosaryReflections
            .Where(r => r.RosaryTypeId == schedule.RosaryTypeId && r.LanguageId == (int)language)
            .ToListAsync(ct);

        RosaryDto result = new()
        {
            RosaryTypeId = schedule.RosaryType.Id,
            RosaryTypeName = schedule.RosaryType.Name,
            DayOfWeek = dayNameText,
            RosaryReflections = reflectionEntities.Select(r => new RosaryReflectionDto
            {
                Id = r.Id,
                RosaryTypeId = r.RosaryTypeId,
                Title = r.Title,
                Content = r.Content
            }).ToArray()
        };

        return result;
    }

    public async Task<IEnumerable<RosaryDto>> GetAllRosariesExceptDateAsync(LanguageTypeEnum language, CancellationToken ct = default)
    {
        DayOfWeek excludeDay = DateTime.UtcNow.DayOfWeek;

        // load schedules for given language excluding the target day, include RosaryType navigation
        var schedules = await _db.RosaryDaySchedules
            .Include(s => s.RosaryType)
            .Where(s => s.LanguageId == (int)language && s.DayOfWeek != excludeDay)
            .ToListAsync(ct);

        if (schedules == null || schedules.Count == 0)
            return Enumerable.Empty<RosaryDto>();

        // distinct rosary type ids from schedules
        var rosaryTypeIds = schedules.Select(s => s.RosaryTypeId).Distinct().ToList();

        // load reflections for those types filtered by language
        var reflectionEntities = await _db.RosaryReflections
            .Where(r => rosaryTypeIds.Contains(r.RosaryTypeId) && r.LanguageId == (int)language)
            .ToListAsync(ct);

        var results = new List<RosaryDto>();

        foreach (var group in schedules.GroupBy(s => s.RosaryTypeId))
        {
            var schedule = group.First();
            string dayNameText = GetPolishDayName(schedule.DayOfWeek);

            var reflectionsForType = reflectionEntities
                .Where(r => r.RosaryTypeId == group.Key)
                .Select(r => new RosaryReflectionDto
                {
                    Id = r.Id,
                    RosaryTypeId = r.RosaryTypeId,
                    Title = r.Title,
                    Content = r.Content
                }).ToArray();

            results.Add(new RosaryDto
            {
                RosaryTypeId = schedule.RosaryType.Id,
                RosaryTypeName = schedule.RosaryType.Name,
                DayOfWeek = dayNameText,
                RosaryReflections = reflectionsForType
            });
        }

        return results;
    }

    private string GetPolishDayName(DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Monday => "Poniedzia³ek",
            DayOfWeek.Tuesday => "Wtorek",
            DayOfWeek.Wednesday => "Œroda",
            DayOfWeek.Thursday => "Czwartek",
            DayOfWeek.Friday => "Pi¹tek",
            DayOfWeek.Saturday => "Sobota",
            DayOfWeek.Sunday => "Niedziela",
            _ => dayOfWeek.ToString()
        };
    }
}
