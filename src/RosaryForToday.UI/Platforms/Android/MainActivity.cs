using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace RosaryForToday.UI
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Force respecting display cutouts
            if (OperatingSystem.IsAndroidVersionAtLeast(28))
            {
                if (Window?.Attributes is { } attributes)
                {
                    attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Default;
                }
            }
        }
    }
}
