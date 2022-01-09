using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace WielkiQuizWiedzy
{
    [Activity(Label = "Score", Theme = "@style/AppTheme")]  
    public class Score: Activity {  
        ListView listView;  
        protected override void OnCreate(Bundle savedInstanceState) {  
            base.OnCreate(savedInstanceState);  
            SetContentView(Resource.Layout.score_layout);  
            listView = FindViewById < ListView > (Resource.Id.lstView);  
            DbHelper db = new DbHelper(this);  
            List <Ranking> lstRanking = db.GetRanking();  
            if (lstRanking.Count > 0) {  
                CustomAdapter adapter = new CustomAdapter(this, lstRanking);  
                listView.Adapter = adapter;  
            }  
        }  
    }  
} 