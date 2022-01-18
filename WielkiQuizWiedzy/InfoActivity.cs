using Android.App;
using Android.Content.PM;
using Android.OS;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "InfoActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class InfoActivity: Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.info_layout);
        }
            
    }
}