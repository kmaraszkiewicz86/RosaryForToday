using Android.Content;
using Android.Widget;
using static Android.Widget.RemoteViewsService;

namespace RosaryForToday.UI
{
    public class RosaryListRemoteViewsFactory : Java.Lang.Object, IRemoteViewsFactory
    {
        private Context _context;
        private List<string> _rosaryList = [];
        private RemoteViews _loadingView;

        public RosaryListRemoteViewsFactory(Context context)
        {
            _context = context;
            _rosaryList = new List<string>();
            _loadingView = new RemoteViews(_context.PackageName, Resource.Layout.rosary_today_widget_layout);
        }

        public int Count => _rosaryList.Count;

        public void OnCreate() { }

        public void OnDataSetChanged()
        {
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
                return null!;

            // Zwróciæ layout dla JEDNEGO wiersza listy
            RemoteViews views = new(_context.PackageName, Resource.Layout.rosary_today_widget_list_item);

            // Ustawiæ tekst dla bie¿¹cego elementu
            views.SetTextViewText(Resource.Id.rosaryItemName, _rosaryList[position]);

            // Opcjonalnie: klik na element
            var intent = new Intent();
            intent.PutExtra("rosary_name", _rosaryList[position]);
            views.SetOnClickFillInIntent(Resource.Id.rosaryItemName, intent);

            return views;
        }

        public RemoteViews GetLoadingView() => LoadingView ?? null!;

        public int ViewTypeCount => 1;

        public long GetItemId(int position) => position;

        public bool HasStableIds => true;

        public RemoteViews? LoadingView => _loadingView;
    }
}