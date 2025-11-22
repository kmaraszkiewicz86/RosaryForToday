using System.Windows.Input;
using RosaryForToday.Presentation.ViewModels;

public class RosaryViewModel : BindableObject
{
    private bool _showDetails = false;

    public bool ShowDetails
    {
        get => _showDetails;
        set
        {
            if (_showDetails == value) return;
            _showDetails = value;
            OnPropertyChanged();
        }
    }

    public ICommand ToggleDetailsCommand { get; }

    public RosaryViewModel()
    {
        ToggleDetailsCommand = new Command(() => ShowDetails = !ShowDetails);
    }

    public int RosaryTypeId { get; set; }
    public string RosaryTypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DayOfWeek { get; set; } = string.Empty;
    public RosaryReflectionViewModel[] RosaryReflections { get; set; } = Array.Empty<RosaryReflectionViewModel>();
}
