using Android.App;
using Android.OS;

namespace WielkiQuizWiedzy
{
    public class GameActivity: Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.game_layout);
        }
        
    }
}