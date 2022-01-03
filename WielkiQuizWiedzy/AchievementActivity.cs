using Android.App;
using Android.Content.PM;
using Android.OS;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "AchievementActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AchievementActivity: Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.achievement_layout);
        }
            
    }
}
