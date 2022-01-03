using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button startButton = FindViewById<Button>(Resource.Id.buttonStart);
            startButton!.Click += StartGame;

            Button infoButton = FindViewById<Button>(Resource.Id.buttonInfo);
            infoButton!.Click += infoButtonOnClick;

            Button achievementButton = FindViewById<Button>(Resource.Id.buttonStats);
            achievementButton!.Click += achievementButtonOnClick;

            Button exitButton = FindViewById<Button>(Resource.Id.buttonExit);
            exitButton!.Click += closeApplication;

        }

        private void StartGame(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(GameActivity));
            this.StartActivity(intent);
        }
        private void infoButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(InfoActivity));
            this.StartActivity(intent);
        }
        
        private void achievementButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(AchievementActivity));
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