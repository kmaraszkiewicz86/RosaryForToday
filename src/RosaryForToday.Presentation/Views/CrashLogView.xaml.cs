namespace RosaryForToday.Presentation.Views;

public partial class CrashLogView : ContentPage
{
	public CrashLogView(CrashLogViewModel crashLogViewModel)
	{
		InitializeComponent();
		BindingContext = crashLogViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CrashLogViewModel vm)
        {
            await vm.LoadAsync();
        }
    }
}