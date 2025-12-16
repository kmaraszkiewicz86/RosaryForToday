using Android.App;
using Android.Content;
using Android.Widget;
using System.Collections.Generic;

namespace RosaryForToday.UI.Platforms.Android
{
    [Service(Exported = true, Permission = "android.permission.BIND_REMOTEVIEWS")]
    [IntentFilter(new[] { "android.content.RemoteViewsService" })]
    public class RosaryWidgetRemoteViewsService : RemoteViewsService
    {
        public override IRemoteViewsFactory OnGetViewFactory(Intent intent)
        {
            return new RosaryListRemoteViewsFactory(this.ApplicationContext);
        }
    }

    public class RosaryListRemoteViewsFactory : Java.Lang.Object, IRemoteViewsFactory
    {
        private Context _context;
        private List<string> _rosaryList;

        public RosaryListRemoteViewsFactory(Context context)
        {
            _context = context;
            _rosaryList = new List<string>();
        }

        public int Count => _rosaryList.Count;

        public void OnCreate() { }

        public void OnDataSetChanged()
        {
            // Tutaj za³aduj dane z aplikacji (np. z bazy danych lub API)
            // Na razie przyk³ad ze statycznymi danymi
            _rosaryList.Clear();
            _rosaryList.Add("Tajemnica 1");
            _rosaryList.Add("Tajemnica 2");
            _rosaryList.Add("Tajemnica 3");
            _rosaryList.Add("Tajemnica 4");
            _rosaryList.Add("Tajemnica 5");
        }

        public void OnDestroy() { }

        public RemoteViews GetViewAt(int position)
        {
            if (position < 0 || position >= _rosaryList.Count)
                return null;

            var views = new RemoteViews(_context.PackageName, Resource.Layout.rosary_today_widget_list_item);
            views.SetTextViewText(Resource.Id.rosaryItemName, _rosaryList[position]);

            // Opcjonalnie: klik na element
            var intent = new Intent();
            intent.PutExtra("rosary_name", _rosaryList[position]);
            views.SetOnClickFillInIntent(Resource.Id.rosaryItemName, intent);

            return views;
        }

        public RemoteViews GetLoadingView() => null;

        public int ViewTypeCount => 1;

        public long GetItemId(int position) => position;

        public bool HasStableIds => true;
    }
}