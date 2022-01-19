using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using Java.Lang;
using Java.Util;
using Xamarin.Forms;
using Button = Android.Widget.Button;
using Process = Android.OS.Process;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    
    public class MainActivity : AppCompatActivity {
        
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button infoButton = FindViewById<Button>(Resource.Id.buttonInfo);
            infoButton!.Click += infoButtonOnClick;
            
            Button StartButton = FindViewById<Button>(Resource.Id.buttonStart);
            StartButton!.Click += StartGame;
            
            Button exitButton = FindViewById<Button>(Resource.Id.buttonExit);
            exitButton!.Click += closeApplication;
            
            Button btnScore = FindViewById<Button>(Resource.Id.btnScore);
            btnScore.Click += ScoreBoard;

        }
        
        private void StartGame(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(GameActivity));
            this.StartActivity(intent);
        }

        private void ScoreBoard(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(Score));
            this.StartActivity(intent);
        }

        private void infoButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(InfoActivity));
            this.StartActivity(intent);
        }

        private void closeApplication(object sender, EventArgs eventArgs)
        {
            Process.KillProcess(Process.MyPid());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}