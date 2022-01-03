using Android.App;
using Android.Content.PM;
using Android.OS;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "InfoActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class InfoActivity: Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.info_layout);
        }
            
    }
}