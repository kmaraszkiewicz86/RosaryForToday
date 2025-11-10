using SimpleCqrs;

namespace RosaryForToday.Models.Queries;

public class GetRosaryTypesQuery : IQuery<List<RosaryTypeDto>>
{
    public int? LanguageId { get; set; }
}

public class RosaryTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int LanguageId { get; set; }
}