using SimpleCqrs;

namespace RosaryForToday.Application.Commands;

public class CreateRosaryTypeCommand : ICommand<int>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int LanguageId { get; set; }
}
