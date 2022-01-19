using System.ComponentModel;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace WielkiQuizWiedzy
{
[Activity(Label = "Done", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]  
    public class Done: Activity {  
        Button btnTryAgain;  
        TextView txtTotalQuestion, txtTotalScore;  
        ProgressBar progressBarResult;
        protected override void OnCreate(Bundle savedInstanceState) {  
            base.OnCreate(savedInstanceState);  
            SetContentView(Resource.Layout.done_layout);  
            DbHelper db = new DbHelper(this);  
            btnTryAgain = FindViewById < Button > (Resource.Id.btnTryAgain);  
            txtTotalQuestion = FindViewById < TextView > (Resource.Id.txtTotalQuestion);  
            txtTotalScore = FindViewById < TextView > (Resource.Id.txtTotalScore);  
            progressBarResult = FindViewById < ProgressBar > (Resource.Id.progressBardone);  
            btnTryAgain.Click += delegate
            {
                Intent intent = new Intent(this, typeof(GameActivity));  
                StartActivity(intent);  
                Finish();  
            };  
            //Get Data  
            Bundle bundle = Intent.Extras;  
            if (bundle != null) {  
                int score = bundle.GetInt("SCORE");  
                int totalQuestion = bundle.GetInt("TOTAL");  
                int coreectAnswer = bundle.GetInt("CORRECT");
                
                if (totalQuestion == 50) // Easy Mode 
                {  
                    score += (score/10);  
                } 
                
                else if (totalQuestion == 100) // Medium Mode  
                {  
                    score += (score/10)*3;  
                } 
                
                else if (totalQuestion == 150) // Hard Mode  
                {  
                    score += (score/10)*5;  
                }

                txtTotalScore.Text = $"WYNIK :{ score.ToString("  0.00 ")}";  
                txtTotalQuestion.Text = $"POPRAWNE : {coreectAnswer}/{totalQuestion}";  
                progressBarResult.Max = totalQuestion;  
                progressBarResult.Progress = coreectAnswer;
                //Save Score  
                db.InsertScore(score);
            }  
        }  
    }  
}  