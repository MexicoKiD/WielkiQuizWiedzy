using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database.Sqlite;
using Android.OS;
using Android.Widget;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "GameActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameActivity : Activity, SeekBar.IOnSeekBarChangeListener
    {
        private Button btnPlay;
        private DbHelper db;
        private SeekBar seekBar;
        private SQLiteDatabase sqLiteDB;
        private TextView txtMode;

        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            txtMode.Text = getPlayMode().ToUpper();
        }

        public void OnStartTrackingTouch(SeekBar seekBar)
        {
        }

        public void OnStopTrackingTouch(SeekBar seekBar)
        {
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.game_layout);

            db = new DbHelper(this);
            sqLiteDB = db.WritableDatabase;
            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);
            txtMode = FindViewById<TextView>(Resource.Id.Mode);
            btnPlay = FindViewById<Button>(Resource.Id.buttonPlay);
            btnPlay.Click += delegate
            {
                var intent = new Intent(this, typeof(Playing));
                intent.PutExtra("MODE", getPlayMode());
                StartActivity(intent);
                Finish();
            };
            seekBar.SetOnSeekBarChangeListener(this);
        }

        private string getPlayMode()
        {
            if (seekBar.Progress == 0) return Common.Mode.ŁATWY.ToString();
            if (seekBar.Progress == 1) return Common.Mode.ŚREDNI.ToString();
            if (seekBar.Progress == 2) return Common.Mode.TRUDNY.ToString();
            return Common.Mode.NAJTRUDNIEJSZY.ToString();
        }
    }
}