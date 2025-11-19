using RosaryForToday.Presentation.Views;

namespace RosaryForToday.UI;

public partial class AppShell : Shell
{
    public AppShell(RosaryListView main)
    {
        InitializeComponent();
        // set main page
        MainPage = main;
    }
}
