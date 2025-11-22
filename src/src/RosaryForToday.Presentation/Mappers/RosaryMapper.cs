using Riok.Mapperly;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Presentation.Models;

namespace RosaryForToday.Presentation.Mappers;

[Mapper]
public static partial class RosaryMapper
{
    public static partial RosaryForTodayViewModel Map(RosaryForTodayDto dto);
    public static partial RosaryReflectionViewModel Map(RosaryReflectionDto dto);
    public static partial RosaryReflectionViewModel[] Map(RosaryReflectionDto[] dtos);
}
