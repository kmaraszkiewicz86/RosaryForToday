using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using RosaryForToday.Models.Enums;
using RosaryForToday.Models.Queries;
using RosaryForToday.UI.Platforms.Android;
using SimpleCqrs;
using System;
using static System.Net.Mime.MediaTypeNames;

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
            Log.Info("RosaryWidget", $"OnUpdate called, ids length={appWidgetIds?.Length ?? 0}");

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
                    Log.Info("RosaryWidget", "ACTION_REFRESH received");

                    var appWidgetManager = AppWidgetManager.GetInstance(context);
                    var component = new ComponentName(context!, Java.Lang.Class.FromType(typeof(RosaryTodayAppWidgetProvider)));
                    var ids = appWidgetManager!.GetAppWidgetIds(component);

                    Log.Info("RosaryWidget", $"Widget count={ids?.Length ?? 0}");

                    foreach (var id in ids!)
                    {
                        Log.Info("RosaryWidget", $"Updating widget id={id}");
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

        private static void UpdateWidget(Context context, AppWidgetManager appWidgetManager, int appWidgetId)
        {
            Log.Info("RosaryWidget", $"UpdateWidget appWidgetId={appWidgetId}");

            var views = new RemoteViews(context.PackageName, Resource.Layout.rosary_today_widget_layout);

            UpdateRosaryText(views);

            // Lista elementów rozwa¿añ ró¿añcowych
            var serviceIntent = new Intent(context, typeof(RosaryWidgetRemoteViewsService));
            serviceIntent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetId);
            views.SetRemoteAdapter(Resource.Id.rosaryListView, serviceIntent);

            var flags = Build.VERSION.SdkInt >= BuildVersionCodes.S
                ? PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent   
                : PendingIntentFlags.UpdateCurrent;

            // Intent odœwie¿aj¹cy wid¿et – u¿ywany przez przycisk
            var refreshIntent = new Intent(context, typeof(RosaryTodayAppWidgetProvider));
            refreshIntent.SetAction(ActionRefresh);

            var refreshPending = PendingIntent.GetBroadcast(context, 0, refreshIntent, flags);

            // Po klikniêciu PRZYCISKU odœwie¿ listê
            views.SetOnClickPendingIntent(Resource.Id.widgetButton, refreshPending);

            // Przycisk otwieraj¹cy aplikacjê
            var openAppPending = CreateOpenAppPendingIntent(context, flags);
            views.SetOnClickPendingIntent(Resource.Id.widgetOpenAppButton, openAppPending);

            appWidgetManager.NotifyAppWidgetViewDataChanged(appWidgetId, Resource.Id.rosaryListView);
            appWidgetManager.UpdateAppWidget(appWidgetId, views);

            Log.Info("RosaryWidget", "Widget updated");
        }

        private static PendingIntent CreateOpenAppPendingIntent(Context context, PendingIntentFlags flags)
        {
            var launchIntent = context.PackageManager?.GetLaunchIntentForPackage(context.PackageName);
            Log.Info("RosaryWidget", $"LaunchIntent from package manager is null? {launchIntent is null}");

            if (launchIntent is null)
            {
                launchIntent = new Intent(context, typeof(MainActivity));
                launchIntent.AddCategory(Intent.CategoryLauncher);
                launchIntent.SetAction(Intent.ActionMain);
                Log.Info("RosaryWidget", "Using explicit MainActivity launch intent");
            }

            var pending = PendingIntent.GetActivity(context, 0, launchIntent, flags);
            Log.Info("RosaryWidget", $"PendingIntent created: {pending != null}");

            return pending!;
        }

        private static void UpdateRosaryText(RemoteViews views)
        {
            var mediator = ServiceLocator.Services?.GetService<ISimpleMediator>();

            if (mediator is null)
                return;

            string text = mediator.GetQuery(new GetRosaryTitleForTodayQuery { Language = LanguageTypeEnum.Polish });
            views.SetTextViewText(Resource.Id.widgetTitle, text);
        }
    }
}