using Android.App;
using Android.Content.PM;
using Android.OS;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "GameActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameActivity: Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.game_layout);
        }
        
    }
}