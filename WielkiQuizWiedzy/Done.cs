using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "Done", Theme = "@style/Theme.AppCompat.Light.NoActionBar",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class Done : Activity
    {
        private Button btnTryAgain;
        private ProgressBar progressBarResult;
        private TextView txtTotalQuestion, txtTotalScore;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.done_layout);
            var db = new DbHelper(this);
            btnTryAgain = FindViewById<Button>(Resource.Id.buttonTryAgain);
            txtTotalQuestion = FindViewById<TextView>(Resource.Id.TotalQuestion);
            txtTotalScore = FindViewById<TextView>(Resource.Id.TotalScore);
            progressBarResult = FindViewById<ProgressBar>(Resource.Id.progressBarDone);
            btnTryAgain.Click += delegate
            {
                var intent = new Intent(this, typeof(GameActivity));
                StartActivity(intent);
                Finish();
            };
            //Get Data  
            var bundle = Intent.Extras;
            if (bundle != null)
            {
                var score = bundle.GetInt("SCORE");
                var totalQuestion = bundle.GetInt("TOTAL");
                var coreectAnswer = bundle.GetInt("CORRECT");

                if (totalQuestion == 50) // Easy Mode 
                    score += score / 10;

                else if (totalQuestion == 100) // Medium Mode  
                    score += score / 10 * 3;

                else if (totalQuestion == 150) // Hard Mode  
                    score += score / 10 * 5;

                txtTotalScore.Text = $"WYNIK :{score.ToString("  0.00 ")}";
                txtTotalQuestion.Text = $"POPRAWNE : {coreectAnswer}/{totalQuestion}";
                progressBarResult.Max = totalQuestion;
                progressBarResult.Progress = coreectAnswer;
                //Save Score  
                db.InsertScore(score);
            }
        }
    }
}