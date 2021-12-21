using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button infoButton = FindViewById<Button>(Resource.Id.button3);
            infoButton.Click += infoButtonOnClick;

            Button achievementButton = FindViewById<Button>(Resource.Id.button2);
            achievementButton.Click += achievementButtonOnClick;

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
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}