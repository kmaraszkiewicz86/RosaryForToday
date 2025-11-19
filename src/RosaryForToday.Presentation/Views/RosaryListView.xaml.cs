using RosaryForToday.Presentation.ViewModels;

namespace RosaryForToday.Presentation.Views;

public partial class RosaryListView : ContentPage
{
    public RosaryListView(RosaryListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RosaryListViewModel vm)
            await vm.LoadAsync();
    }
}
        