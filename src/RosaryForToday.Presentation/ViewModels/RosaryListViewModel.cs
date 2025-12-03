using RosaryForToday.ApplicationLayer.QueryHandlers;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;
using RosaryForToday.Models.Queries;
using RosaryForToday.Presentation.Mappers;
using SimpleCqrs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RosaryForToday.Presentation.ViewModels;

public class RosaryListViewModel(IRosaryDbQuery _dbQuery, ISimpleMediator _mediator) : BindableObject
{
    private string? _errorMessage;
    private bool _showAllItems = false;

    public ObservableCollection<RosaryViewModel> Items { get; } = new();

    public ObservableCollection<RosaryViewModel> AllItems { get; } = new();

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

    public ICommand RefreshCommand => new Command(async () => await RefreshAsync());
    public ICommand ShowCrashLogCommand => new Command(async () => await OnShowCrashLogAsync());

    public ICommand ShowTodayCommand => new Command(async () => await RefreshAsync());
    public ICommand ShowAllCommand => new Command(async () => await RefreshAsync());

    public async Task LoadAsync()
    {
        try
        {
            ErrorMessage = null;

            await LoadTodayRosariesAsync();
            await LoadAllRosariesAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"B³¹d: {ex.Message}";
        }
    }

    private async Task LoadTodayRosariesAsync()
    {
        // Use query handlers instead of calling the DB query directly
        var todayHandler = new GetRosaryForTodayQueryHandler(_dbQuery);
        var todayQuery = new GetRosaryForTodayQuery { Language = LanguageTypeEnum.Polish };
        RosaryDto? todayResult = await todayHandler.HandleAsync(todayQuery);

        if (todayResult is null)
        {
            ErrorMessage = "Brak danych dla dzisiejszej daty.";
        }

        Items.Clear();
        if (todayResult is not null)
        {
            // Map DTO to ViewModel using Mapperly generated mapper
            RosaryViewModel rosaryViewModel = RosaryMapper.ToRosaryViewModel(todayResult);
            // initialize toggling command on each item if required
            if (rosaryViewModel is not null)
            {
                Items.Add(rosaryViewModel);
            }
        }
    }

    private async Task LoadAllRosariesAsync()
    {
        // Load all rosaries into AllItems using GetAllRosariesQueryHandler
        GetAllRosariesQueryHandler allHandler = new(_dbQuery);
        GetAllRosariesQuery allQuery = new() { Language = LanguageTypeEnum.Polish };
        IEnumerable<RosaryDto> allResults = await allHandler.HandleAsync(allQuery);

        AllItems.Clear();
        foreach (var rosary in allResults)
        {
            // Map DTO to ViewModel using Mapperly generated mapper
            RosaryViewModel rosaryViewModel = RosaryMapper.ToRosaryViewModel(rosary);
            // initialize toggling command on each item if required
            if (rosaryViewModel is not null)
            {
                AllItems.Add(rosaryViewModel);
            }
        }
    }

    private async Task OnShowCrashLogAsync()
    {
        // Shell requires absolute route for ShellContent — prefix with "///"
        await Shell.Current.GoToAsync("///CrashLog");
    }

    private async Task RefreshAsync()
    {
        Items.Clear();
        AllItems.Clear();
        await LoadAsync();
    }
}