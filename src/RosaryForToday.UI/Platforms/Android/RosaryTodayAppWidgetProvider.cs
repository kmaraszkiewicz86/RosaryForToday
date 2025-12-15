using System;
using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using Android.OS;
using RosaryForToday.UI;

namespace RosaryForToday.Presentation.Platforms.Android
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
                UpdateWidget(context!, appWidgetManager!, appWidgetId, $"Rosary: {DateTime.Now:T}");
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
                    UpdateWidget(context!, appWidgetManager!, id, $"Refreshed: {DateTime.Now:T}");
                }
            }
        }

        static void UpdateWidget(Context context, AppWidgetManager appWidgetManager, int appWidgetId, string text)
        {
            var views = new RemoteViews(context.PackageName, Resource.Layout.rosary_today_widget_layout);
            views.SetTextViewText(Resource.Id.widgetText, text);

            // Click opens MainActivity
            var intent = new Intent(context, typeof(MainActivity));
            intent.SetAction(Intent.ActionMain);
            intent.AddCategory(Intent.CategoryLauncher);

            var flags = Build.VERSION.SdkInt >= BuildVersionCodes.S
                ? PendingIntentFlags.Immutable | PendingIntentFlags.UpdateCurrent
                : PendingIntentFlags.UpdateCurrent;


            var pending = PendingIntent.GetActivity(context, 0, intent, flags);
            views.SetOnClickPendingIntent(Resource.Id.widgetButton, pending);

            // Optional: click on the whole widget to trigger a refresh broadcast
            var refreshIntent = new Intent(context, typeof(RosaryTodayAppWidgetProvider));
            refreshIntent.SetAction(ActionRefresh);
            var refreshPending = PendingIntent.GetBroadcast(context, 1, refreshIntent, flags);
            views.SetOnClickPendingIntent(Resource.Id.widgetText, refreshPending);

            appWidgetManager.UpdateAppWidget(appWidgetId, views);
        }
    }
}