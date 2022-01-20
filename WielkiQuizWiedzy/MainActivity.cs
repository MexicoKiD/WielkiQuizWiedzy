using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Xamarin.Essentials;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var infoButton = FindViewById<Button>(Resource.Id.buttonInfo);
            infoButton!.Click += infoButtonOnClick;

            var StartButton = FindViewById<Button>(Resource.Id.buttonStart);
            StartButton!.Click += StartGame;

            var exitButton = FindViewById<Button>(Resource.Id.buttonExit);
            exitButton!.Click += closeApplication;

            var btnScore = FindViewById<Button>(Resource.Id.buttonScore);
            btnScore.Click += ScoreBoard;
        }

        private void StartGame(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(GameActivity));
            StartActivity(intent);
        }

        private void ScoreBoard(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(Score));
            StartActivity(intent);
        }

        private void infoButtonOnClick(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(this, typeof(InfoActivity));
            StartActivity(intent);
        }

        private void closeApplication(object sender, EventArgs eventArgs)
        {
            Process.KillProcess(Process.MyPid());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}