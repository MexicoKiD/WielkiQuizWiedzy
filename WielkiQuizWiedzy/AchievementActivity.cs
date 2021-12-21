using Android.App;
using Android.OS;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "AchievementActivity")]
    public class AchievementActivity: Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.achievement_layout);
        }
            
    }
}
