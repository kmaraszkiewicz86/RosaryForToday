using Riok.Mapperly.Abstractions;
using RosaryForToday.Models.Dtos;

namespace RosaryForToday.Presentation.Mappers;

[Mapper]
public static partial class RosaryMapper
{
    public static partial RosaryViewModel ToRosaryViewModel(RosaryDto dto);
}
