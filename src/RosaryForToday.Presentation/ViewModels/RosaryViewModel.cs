using System.Windows.Input;
using RosaryForToday.Presentation.ViewModels;

public class RosaryViewModel : DetailsBaseViewModel
{
    public int RosaryTypeId { get; set; }
    public string RosaryTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DayOfWeek { get; set; } = string.Empty;
    public RosaryReflectionViewModel[] RosaryReflections { get; set; } = Array.Empty<RosaryReflectionViewModel>();

    protected override string ButtonShowText => "Poka¿";

    protected override string ButtonHideText => "Ukryj";
}
