using Riok.Mapperly.Abstractions;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Presentation.ViewModels;

namespace RosaryForToday.Presentation.Mappers;

[Mapper]
public static partial class RosaryReflectionMapper
{
    public static partial RosaryReflectionViewModel ToRosaryReflectionViewModel(RosaryReflectionDto dto);
    public static partial RosaryReflectionViewModel[] ToRosaryReflectionViewModels(RosaryReflectionDto[] dtos);
}
