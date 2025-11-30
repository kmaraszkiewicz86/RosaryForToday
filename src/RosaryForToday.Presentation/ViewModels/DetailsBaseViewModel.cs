using System.Windows.Input;

public abstract class DetailsBaseViewModel : BindableObject
{
    private bool _showDetails = false;

    public string ButtonName => ShowDetails ? ButtonHideText : ButtonShowText;

    public bool ShowDetails
    {
        get => _showDetails;
        set
        {
            if (_showDetails == value) return;
            _showDetails = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ButtonName));
        }
    }

    public ICommand ToggleDetailsCommand { get; }

    protected abstract string ButtonShowText { get; }
    protected abstract string ButtonHideText { get; }

    public DetailsBaseViewModel()
    {
        ToggleDetailsCommand = new Command(() => ShowDetails = !ShowDetails);
    }
}
