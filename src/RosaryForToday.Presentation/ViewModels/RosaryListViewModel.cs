using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RosaryForToday.Presentation.ViewModels;

public class RosaryListViewModel : BindableObject
{
    private readonly IRosaryDbQuery _dbQuery;
    private string? _errorMessage;

    public ObservableCollection<RosaryForTodayDto> Items { get; } = new();

    public string? ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage == value) return;
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public ICommand RefreshCommand { get; }

    public RosaryListViewModel(IRosaryDbQuery dbQuery)
    {
        _dbQuery = dbQuery;
        RefreshCommand = new Command(async () => await RefreshAsync());
    }

    public async Task LoadAsync()
    {
        try
        {
            ErrorMessage = null;
            RosaryForTodayDto? result = await _dbQuery.GetRosaryForDateAsync(LanguageTypeEnum.Polish, DateTime.Today);

            if (result is null)
            {
                ErrorMessage = "Brak danych dla dzisiejszej daty.";
                return;
            }

            Items.Clear();
            Items.Add(result);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"B³¹d: {ex.Message}";
        }
    }

    private async Task RefreshAsync()
    {
        Items.Clear();
        await LoadAsync();
    }
}