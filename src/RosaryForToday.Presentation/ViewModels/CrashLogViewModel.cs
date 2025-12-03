using RosaryForToday.Presentation.Helpers;

public class CrashLogViewModel : BindableObject
{
    private string? _logContent;

    public string LogContent
    {
        get => _logContent ?? string.Empty;
        set
        {
            if (_logContent == value) return;
            _logContent = value;
            OnPropertyChanged();
        }
    }

    public async Task LoadAsync()
    {
        var text = await CrashLogService.GetCrashLogAsync().ConfigureAwait(false);
        LogContent = text ?? "Brak pliku crash.log lub brak zawartoœci.";
    }
}
