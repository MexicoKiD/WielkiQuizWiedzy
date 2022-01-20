using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "Score", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class Score : Activity
    {
        private ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.score_layout);

            listView = FindViewById<ListView>(Resource.Id.lstView);
            var db = new DbHelper(this);
            var lstRanking = db.GetRanking();
            if (lstRanking.Count > 0)
            {
                var adapter = new CustomAdapter(this, lstRanking);
                listView.Adapter = adapter;
            }
        }
    }
}