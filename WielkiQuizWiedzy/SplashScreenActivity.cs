using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "SplashScreenActivity", MainLauncher =true, Theme ="@style/Theme.Splash", NoHistory = true, Icon = "@drawable/splash", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Display Splash Screen for 4 Sec  
            Thread.Sleep(4000);
            //Start Activity1 Activity  
            StartActivity(typeof(MainActivity));
        }
    }
}