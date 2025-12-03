using RosaryForToday.Presentation.Helpers;

namespace RosaryForToday.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                CrashLogService.LogException(e.ExceptionObject as Exception, "AppDomain.CurrentDomain");

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                CrashLogService.LogException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };

#if ANDROID
            Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser += (s, e) =>
            {
                CrashLogService.LogException(e.Exception, "AndroidEnvironment");
            };
#endif
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}