using Android.App;
using Android.Appwidget;
using Android.Content;
using Android.Widget;
using RosaryForToday.UI.Platforms.Android;
using AndroidApplication = Android.App.Application;

namespace RosaryForToday.Presentation.Platforms.Android
{
    public static class WidgetUpdater
    {
        // Call from shared code (via partial platform implementation or DI) to refresh all widgets
        public static void UpdateAll(string text)
        {
            var context = AndroidApplication.Context;
            var appWidgetManager = AppWidgetManager.GetInstance(context);
            var component = new ComponentName(context, Java.Lang.Class.FromType(typeof(RosaryTodayAppWidgetProvider)));
            var ids = appWidgetManager!.GetAppWidgetIds(component);

            foreach (var id in ids!)
            {
                var views = new RemoteViews(context.PackageName, Resource.Layout.rosary_today_widget_layout);
                views.SetTextViewText(Resource.Id.widgetTitle, text);
                appWidgetManager.UpdateAppWidget(id, views);
            }
        }
    }
}