using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database.Sqlite;
using Android.OS;
using Android.Widget;
using System;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "GameActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameActivity: Activity, SeekBar.IOnSeekBarChangeListener
    {
        SeekBar seekBar;  
        TextView txtMode;
        private Button btnPlay;  
        DbHelper db;  
        SQLiteDatabase sqLiteDB;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.game_layout);
            
            db = new DbHelper(this);  
            sqLiteDB = db.WritableDatabase;  
            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);  
            txtMode = FindViewById<TextView>(Resource.Id.txtMode);  
            btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            btnPlay.Click += delegate {  
                Intent intent = new Intent(this, typeof(Playing));  
                intent.PutExtra("MODE", getPlayMode());  
                StartActivity(intent);  
                Finish();  
            };  
            seekBar.SetOnSeekBarChangeListener(this);
        }
        
        private String getPlayMode() {  
            if (seekBar.Progress == 0) return Common.Mode.ŁATWY.ToString();  
            else if (seekBar.Progress == 1) return Common.Mode.ŚREDNI.ToString();  
            else if (seekBar.Progress == 2) return Common.Mode.TRUDNY.ToString();  
            else return Common.Mode.NAJTRUDNIEJSZY.ToString();  
        }  
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser) {  
            txtMode.Text = getPlayMode().ToUpper();  
        }  
        public void OnStartTrackingTouch(SeekBar seekBar) {}  
        public void OnStopTrackingTouch(SeekBar seekBar) {}
        
    }
}