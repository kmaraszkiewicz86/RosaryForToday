using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace RosaryForToday.UI.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true, Label = "RosaryForToday Widget")]
    [IntentFilter(new[] { "android.appwidget.action.APPWIDGET_UPDATE" })]
    public class RosaryTodayAppWidgetProvider : AppWidgetProvider
    {
        public const string ActionRefresh = "RosaryForToday.ACTION_REFRESH_WIDGET";

        public override void OnUpdate(Context? context, AppWidgetManager? appWidgetManager, int[]? appWidgetIds)
        {
            base.OnUpdate(context, appWidgetManager, appWidgetIds);

            foreach (var appWidgetId in appWidgetIds!)
            {
                UpdateWidget(context!, appWidgetManager!, appWidgetId);
            }
        }

        public override void OnReceive(Context? context, Intent? intent)
        {
            base.OnReceive(context, intent);

            if (intent?.Action == ActionRefresh)
            {
                var appWidgetManager = AppWidgetManager.GetInstance(context);
                var component = new ComponentName(context!, Java.Lang.Class.FromType(typeof(RosaryTodayAppWidgetProvider)));
                var ids = appWidgetManager!.GetAppWidgetIds(component);
                foreach (var id in ids!)
                {
                    UpdateWidget(context!, appWidgetManager!, id);
                }
            }
        }

        static void UpdateWidget(Context context, AppWidgetManager appWidgetManager, int appWidgetId)
        {
            var views = new RemoteViews(context.PackageName, Resource.Layout.rosary_today_widget_layout);

            // Ustaw adapter dla ListView
            var intent = new Intent(context, typeof(RosaryWidgetRemoteViewsService));
            intent.PutExtra(AppWidgetManager.ExtraAppwidgetId, appWidgetId);
            views.SetRemoteAdapter(Resource.Id.rosaryListView, intent);
            //views.SetEmptyListView(Resource.Id.rosaryListView, Resource.Id.widgetTitle);

            // Click opens MainActivity
            var mainIntent = new Intent(context, typeof(MainActivity));
            mainIntent.SetAction(Intent.ActionMain);
            mainIntent.AddCategory(Intent.CategoryLauncher);

            var flags = Build.VERSION.SdkInt >= BuildVersionCodes.S
                ? PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
                : PendingIntentFlags.UpdateCurrent;

            var pending = PendingIntent.GetActivity(context, 0, mainIntent, flags);
            views.SetOnClickPendingIntent(Resource.Id.widgetButton, pending);

            appWidgetManager.NotifyAppWidgetViewDataChanged(appWidgetId, Resource.Id.rosaryListView);
            appWidgetManager.UpdateAppWidget(appWidgetId, views);
        }
    }
}