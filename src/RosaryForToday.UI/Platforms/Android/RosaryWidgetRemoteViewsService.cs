using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;

namespace RosaryForToday.UI.Platforms.Android
{
    [Register("com.companyname.rosaryfortoday.ui.RosaryWidgetRemoteViewsService")]
    [Service(Exported = true, Permission = "android.permission.BIND_REMOTEVIEWS")]
    [IntentFilter(new[] { "android.content.RemoteViewsService" })]
    public class RosaryWidgetRemoteViewsService : RemoteViewsService
    {
        public override IRemoteViewsFactory? OnGetViewFactory(Intent? intent)
        {
            if (this.ApplicationContext is null)
                return null;

            return new RosaryListRemoteViewsFactory(this.ApplicationContext);
        }
    }
}