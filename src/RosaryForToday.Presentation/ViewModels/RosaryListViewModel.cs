using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RosaryForToday.Domain.DbQueries;
using RosaryForToday.Models.Dtos;
using RosaryForToday.Models.Enums;

namespace RosaryForToday.Presentation.ViewModels;

public class RosaryListViewModel : BindableObject
{
    private readonly IRosaryDbQuery _dbQuery;

    public ObservableCollection<RosaryForTodayDto> Items { get; } = new ObservableCollection<RosaryForTodayDto>();

    public RosaryListViewModel(IRosaryDbQuery dbQuery)
    {
        _dbQuery = dbQuery;
    }

    public async Task LoadAsync()
    {
        var result = await _dbQuery.GetRosaryForDateAsync(LanguageTypeEnum.Polish, System.DateTime.Today);
        if (result != null)
            Items.Add(result);
    }
}
