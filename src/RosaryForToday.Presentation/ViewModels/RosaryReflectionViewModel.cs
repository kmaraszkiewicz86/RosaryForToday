namespace RosaryForToday.Presentation.ViewModels;

public class RosaryReflectionViewModel : DetailsBaseViewModel
{
    public int Id { get; set; }
    public int RosaryTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    protected override string ButtonShowText => "Poka¿ tajemnice";

    protected override string ButtonHideText => "Ukryj tajemnice";
}