using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RosaryForToday.ApplicationLayer.QueryHandlers;
using RosaryForToday.Models.Queries;

namespace RosaryForToday.Presentation.ViewModels;

public class RosaryListViewModel : BindableObject
{
    private readonly IRosaryDbQuery _dbQuery;
    private string? _errorMessage;
    private bool _showDetails = false;
    private bool _showAllItems = false;

    public ObservableCollection<RosaryForTodayDto> Items { get; } = new();

    // New collection containing all rosaries
    public ObservableCollection<RosaryForTodayDto> AllItems { get; } = new();

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

    // Indicates whether AllItems tab is selected
    public bool ShowAllItems
    {
        get => _showAllItems;
        set
        {
            if (_showAllItems == value) return;
            _showAllItems = value;
            OnPropertyChanged();
        }
    }

    public ICommand RefreshCommand { get; }

    public ICommand ToggleDetailsCommand { get; }

    // Commands to switch tabs
    public ICommand ShowTodayCommand { get; }
    public ICommand ShowAllCommand { get; }

    public RosaryListViewModel(IRosaryDbQuery dbQuery)
    {
        _dbQuery = dbQuery;
        RefreshCommand = new Command(async () => await RefreshAsync());
        ToggleDetailsCommand = new Command(ToggleDetails);
        ShowTodayCommand = new Command(() => ShowAllItems = false);
        ShowAllCommand = new Command(() => ShowAllItems = true);
    }

    public async Task LoadAsync()
    {
        try
        {
            ErrorMessage = null;

            // Use query handlers instead of calling the DB query directly
            var todayHandler = new GetRosaryForTodayQueryHandler(_dbQuery);
            var todayQuery = new GetRosaryForTodayQuery { Language = LanguageTypeEnum.Polish, Date = DateTime.Today };
            RosaryForTodayDto? todayResult = await todayHandler.Handle(todayQuery);

            if (todayResult is null)
            {
                ErrorMessage = "Brak danych dla dzisiejszej daty.";
            }

            Items.Clear();
            if (todayResult is not null)
                Items.Add(todayResult);

            // Load all rosaries into AllItems using GetAllRosariesQueryHandler
            var allHandler = new GetAllRosariesQueryHandler(_dbQuery);
            var allQuery = new GetAllRosariesQuery { Language = LanguageTypeEnum.Polish };
            var allResults = await allHandler.Handle(allQuery);

            AllItems.Clear();
            foreach (var r in allResults)
            {
                AllItems.Add(r);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"B³¹d: {ex.Message}";
        }
    }

    private void ToggleDetails()
    {
        ShowDetails = !ShowDetails;
    }

    private async Task RefreshAsync()
    {
        Items.Clear();
        AllItems.Clear();
        await LoadAsync();
    }
}