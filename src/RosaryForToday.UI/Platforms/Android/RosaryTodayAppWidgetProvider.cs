using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using RosaryForToday.UI.Platforms.Android;
using System;

namespace RosaryForToday.UI
{
    [Register("com.companyname.rosaryfortoday.ui.RosaryTodayAppWidgetProvider")]
    [BroadcastReceiver(Enabled = true, Exported = true, Label = "RosaryForToday Widget")]
    [IntentFilter(new[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    [IntentFilter(new[] { "com.companyname.rosaryfortoday.ui.ACTION_REFRESH_WIDGET" })]
    public class RosaryTodayAppWidgetProvider : AppWidgetProvider
    {
        public const string ActionRefresh = "com.companyname.rosaryfortoday.ui.ACTION_REFRESH_WIDGET";

        public override void OnUpdate(Context? context, AppWidgetManager? appWidgetManager, int[]? appWidgetIds)
        {
            Log.Debug("RosaryWidget", $"OnUpdate called, ids length={appWidgetIds?.Length ?? 0}");

            base.OnUpdate(context, appWidgetManager, appWidgetIds);

            if (context is null || appWidgetManager is null || appWidgetIds is null)
            {
                Log.Error("RosaryWidget", "OnUpdate: context/appWidgetManager/appWidgetIds is null");
                return;
            }

            foreach (var appWidgetId in appWidgetIds)
            {
                UpdateWidget(context, appWidgetManager, appWidgetId);
            }
        }

        public override void OnReceive(Context? context, Intent? intent)
        {
            try
            {
                base.OnReceive(context, intent);

                if (intent?.Action == ActionRefresh)
                {
                    Log.Debug("RosaryWidget", "ACTION_REFRESH received");

                    var appWidgetManager = AppWidgetManager.GetInstance(context);
                    var component = new ComponentName(context!, Java.Lang.Class.FromType(typeof(RosaryTodayAppWidgetProvider)));
                    var ids = appWidgetManager!.GetAppWidgetIds(component);

                    Log.Debug("RosaryWidget", $"Widget count={ids?.Length ?? 0}");

                    foreach (var id in ids!)
                    {
                        Log.Debug("RosaryWidget", $"Updating widget id={id}");
                        UpdateWidget(context!, appWidgetManager!, id);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("RosaryWidget", $"OnReceive exception: {ex}");
                throw;
            }
        }

        static void UpdateWidget(Context context, AppWidgetManager appWidgetManager, int appWidgetId)
        {
            var views = new RemoteViews(context.PackageName, Resource.Layout.rosary_today_widget_layout);

            // Lista
            var serviceIntent = new Intent(context, typeof(RosaryWidgetRemoteViewsService));
            serviceIntent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetId);
            views.SetRemoteAdapter(Resource.Id.rosaryListView, serviceIntent);

            var flags = Build.VERSION.SdkInt >= BuildVersionCodes.S
                ? PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent   
                : PendingIntentFlags.UpdateCurrent;

            // Intent startujacy aplikacje
            var launchIntent = context.PackageManager?.GetLaunchIntentForPackage(context.PackageName);
            Log.Debug("RosaryWidget", $"LaunchIntent from package manager is null? {launchIntent is null}");

            if (launchIntent is null)
            {
                launchIntent = new Intent(context, typeof(MainActivity));
                launchIntent.AddCategory(Intent.CategoryLauncher);
                launchIntent.SetAction(Intent.ActionMain);
                Log.Debug("RosaryWidget", "Using explicit MainActivity launch intent");
            }

            var pending = PendingIntent.GetActivity(context, 0, launchIntent, flags);
            Log.Debug("RosaryWidget", $"PendingIntent created: {pending != null}");

            views.SetOnClickPendingIntent(Resource.Id.widgetButton, pending);
            Log.Debug("RosaryWidget", "ClickPendingIntent set for widgetButton");

            // Refresh po kliknieciu tytulu
            var refreshIntent = new Intent(context, typeof(RosaryTodayAppWidgetProvider));
            refreshIntent.SetAction(ActionRefresh);
            var refreshPending = PendingIntent.GetBroadcast(context, 1, refreshIntent, flags);
            views.SetOnClickPendingIntent(Resource.Id.widgetTitle, refreshPending);

            appWidgetManager.NotifyAppWidgetViewDataChanged(appWidgetId, Resource.Id.rosaryListView);
            appWidgetManager.UpdateAppWidget(appWidgetId, views);

            Log.Debug("RosaryWidget", "Widget updated");
        }
    }
}