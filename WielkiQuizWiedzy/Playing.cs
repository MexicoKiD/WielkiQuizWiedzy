using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using Android.Content;
using Android.Content.PM;
using Xamarin.Forms;
using Button = Android.Widget.Button;
using ProgressBar = Android.Widget.ProgressBar;
using View = Android.Views.View;

namespace WielkiQuizWiedzy
{
[Activity(Label = "Playing", Theme = "@style/Theme.AppCompat.Light.NoActionBar", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]  
    public class Playing: Activity, View.IOnClickListener {  
        const long INTERVAL = 1000;  
        long TIMEOUT;  
        public int progressValue = 0;  
        static CountDown mCountDown;  
        List <Questions> questionPlay = new List <Questions> ();  
        DbHelper db;  
        static int index, score, thisQuestion, totalQuestion, correctAnswer;  
        String mode = String.Empty;  
        //Control  
        public ProgressBar progressBar;  
        TextView txtScore, questionNumber, question;  
        Button btnA, btnB, btnC, btnD;
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);  
            SetContentView(Resource.Layout.playing_layout);  
            //Get data from Main Activity  
            Bundle extra = Intent.Extras;
            if (extra != null) mode = extra.GetString("MODE");
            TIMEOUT = 0;
            index = 0;
            score = 0;
            correctAnswer = 0;
            thisQuestion = 0;

            db = new DbHelper(this);  
            txtScore = FindViewById < TextView > (Resource.Id.txtScore);  
            questionNumber = FindViewById < TextView > (Resource.Id.questionNumber);  
            progressBar = FindViewById < ProgressBar > (Resource.Id.progressBar);  
            question = FindViewById < TextView > (Resource.Id.question_flag);  
            btnA = FindViewById < Button > (Resource.Id.btnAnswerA);  
            btnB = FindViewById < Button > (Resource.Id.btnAnswerB);  
            btnC = FindViewById < Button > (Resource.Id.btnAnswerC);  
            btnD = FindViewById < Button > (Resource.Id.btnAnswerD);  
            btnA.SetOnClickListener(this);  
            btnB.SetOnClickListener(this);  
            btnC.SetOnClickListener(this);  
            btnD.SetOnClickListener(this);
            
            if (mode.Equals(Common.Mode.ŁATWY.ToString())) TIMEOUT = 20000;
            else if (mode.Equals(Common.Mode.ŚREDNI.ToString())) TIMEOUT = 15000;
            else if (mode.Equals(Common.Mode.TRUDNY.ToString())) TIMEOUT = 10000;
            else TIMEOUT = 5000;

            if (TIMEOUT == 20000) progressBar.Max = 20;
            else if (TIMEOUT == 15000) progressBar.Max = 15;
            else if (TIMEOUT == 10000) progressBar.Max = 10;
            else progressBar.Max = 5;
        }
        
        public void OnClick(View v) {  
            mCountDown.Cancel();  
            if (index < totalQuestion) {  
                Button btnClicked = (Button) v;  
                if (btnClicked.Text.Equals(questionPlay[index].CorrectAnswer)) {  
                    score += 10;  
                    correctAnswer++;  
                    ShowQuestion(++index);  
                } else ShowQuestion(++index);  
            }  
            txtScore.Text = $"{score}";  
        }
        
        public void ShowQuestion(int index) {  
            if (index < totalQuestion) {  
                thisQuestion++;  
                questionNumber.Text = $"{thisQuestion}/{totalQuestion}";  
                progressBar.Progress = progressValue = 0;  
                question.Text = questionPlay[index].Question;
                btnA.Text = questionPlay[index].AnswerA;  
                btnB.Text = questionPlay[index].AnswerB;  
                btnC.Text = questionPlay[index].AnswerC;  
                btnD.Text = questionPlay[index].AnswerD;  
                mCountDown.Start();
            } else
            {
                Intent intent = new Intent(this, typeof(Done));  
                Bundle dataSend = new Bundle();  
                dataSend.PutInt("SCORE", score);  
                dataSend.PutInt("TOTAL", totalQuestion);  
                dataSend.PutInt("CORRECT", correctAnswer);  
                intent.PutExtras(dataSend);  
                StartActivity(intent);  
                Finish();  
            }  
        }  
        protected override void OnResume() {  
            base.OnResume();  
            questionPlay = db.GetQuestionMode(mode);  
            totalQuestion = questionPlay.Count;
            mCountDown = new CountDown(this, TIMEOUT, INTERVAL);  
            ShowQuestion(index);  
        }  
        class CountDown: CountDownTimer {  
            Playing playing;  
            public CountDown(Playing playing, long totalTime, long interval): base(totalTime, interval) {  
                this.playing = playing;  
            }  
            public override void OnFinish() {  
                Cancel();  
                playing.ShowQuestion(++index);  
            }  
            public override void OnTick(long millisUntilFinished) {
                playing.progressBar.Progress = playing.progressValue;
                playing.progressValue++;
            }  
        }
        public override void OnBackPressed(){}
    }  
}  