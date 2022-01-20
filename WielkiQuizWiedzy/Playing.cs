using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "Playing", Theme = "@style/Theme.AppCompat.Light.NoActionBar", NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class Playing : Activity, View.IOnClickListener
    {
        private const long INTERVAL = 1000;
        private static CountDown mCountDown;
        private static int index, score, thisQuestion, totalQuestion, correctAnswer;
        private Button btnA, btnB, btnC, btnD;
        private DbHelper db;

        private string mode = string.Empty;

        //Control  
        public ProgressBar progressBar;
        public int progressValue;
        private List<Questions> questionPlay = new List<Questions>();
        private long TIMEOUT;
        private TextView txtScore, questionNumber, question;

        public void OnClick(View v)
        {
            mCountDown.Cancel();
            if (index < totalQuestion)
            {
                var btnClicked = (Button) v;
                if (btnClicked.Text.Equals(questionPlay[index].CorrectAnswer))
                {
                    score += 10;
                    correctAnswer++;
                    ShowQuestion(++index);
                }
                else
                {
                    ShowQuestion(++index);
                }
            }

            txtScore.Text = $"{score}";
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.playing_layout);
            //Get data from Main Activity  
            var extra = Intent.Extras;
            if (extra != null) mode = extra.GetString("MODE");
            TIMEOUT = 0;
            index = 0;
            score = 0;
            correctAnswer = 0;
            thisQuestion = 0;

            db = new DbHelper(this);
            txtScore = FindViewById<TextView>(Resource.Id.Score);
            questionNumber = FindViewById<TextView>(Resource.Id.questionNumber);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            question = FindViewById<TextView>(Resource.Id.question);
            btnA = FindViewById<Button>(Resource.Id.buttonAnswerA);
            btnB = FindViewById<Button>(Resource.Id.buttonAnswerB);
            btnC = FindViewById<Button>(Resource.Id.buttonAnswerC);
            btnD = FindViewById<Button>(Resource.Id.buttonAnswerD);
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

        public void ShowQuestion(int index)
        {
            if (index < totalQuestion)
            {
                thisQuestion++;
                questionNumber.Text = $"{thisQuestion}/{totalQuestion}";
                progressBar.Progress = progressValue = 0;
                question.Text = questionPlay[index].Question;
                btnA.Text = questionPlay[index].AnswerA;
                btnB.Text = questionPlay[index].AnswerB;
                btnC.Text = questionPlay[index].AnswerC;
                btnD.Text = questionPlay[index].AnswerD;
                mCountDown.Start();
            }
            else
            {
                var intent = new Intent(this, typeof(Done));
                var dataSend = new Bundle();
                dataSend.PutInt("SCORE", score);
                dataSend.PutInt("TOTAL", totalQuestion);
                dataSend.PutInt("CORRECT", correctAnswer);
                intent.PutExtras(dataSend);
                StartActivity(intent);
                Finish();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            questionPlay = db.GetQuestionMode(mode);
            totalQuestion = questionPlay.Count;
            mCountDown = new CountDown(this, TIMEOUT, INTERVAL);
            ShowQuestion(index);
        }

        public override void OnBackPressed()
        {
        }

        private class CountDown : CountDownTimer
        {
            private readonly Playing playing;

            public CountDown(Playing playing, long totalTime, long interval) : base(totalTime, interval)
            {
                this.playing = playing;
            }

            public override void OnFinish()
            {
                Cancel();
                playing.ShowQuestion(++index);
            }

            public override void OnTick(long millisUntilFinished)
            {
                playing.progressBar.Progress = playing.progressValue;
                playing.progressValue++;
            }
        }
    }
}