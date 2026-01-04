using Android.Content;
using Android.Widget;
using RosaryForToday.Models.Enums;
using RosaryForToday.Models.Queries;
using SimpleCqrs;
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

            var mediator = ServiceLocator.Services?.GetService<ISimpleMediator>();
            if (mediator != null)
            {
                var rosaryInfo = mediator.GetQueryAsync(new GetRosaryForTodayQuery { Language = LanguageTypeEnum.Polish })
                    .GetAwaiter()
                    .GetResult();

                if (rosaryInfo is null || !rosaryInfo.RosaryReflections.Any())
                    return;

                _rosaryList.AddRange(rosaryInfo.RosaryReflections.Select(r => r.Title));
            }
            else
            {
                _rosaryList.Add(_context.GetString(Resource.String.widget_open_app_message));
            }
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