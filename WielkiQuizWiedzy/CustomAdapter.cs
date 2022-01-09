using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace WielkiQuizWiedzy
{
    class CustomAdapter: BaseAdapter {  
        private Context context;  
        private List < Ranking > lstRanking;  
        public CustomAdapter(Context context, List < Ranking > lstRanking) {  
            this.context = context;  
            this.lstRanking = lstRanking;  
        }  
        public override int Count {  
            get {  
                return lstRanking.Count;  
            }  
        }  
        public override Java.Lang.Object GetItem(int position) {  
            return position;  
        }  
        public override long GetItemId(int position) {  
            return position;  
        }  
        public override View GetView(int position, View convertView, ViewGroup parent) {  
            LayoutInflater inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);  
            View view = inflater.Inflate(Resource.Layout.row, null);  
            TextView txtTop = view.FindViewById < TextView > (Resource.Id.txtTop);  
            ImageView imageTop = view.FindViewById < ImageView > (Resource.Id.imgTop);  
            if (position == 0) imageTop.SetBackgroundResource(Resource.Drawable.splash);  
            else if (position == 1) imageTop.SetBackgroundResource(Resource.Drawable.splash);  
            else imageTop.SetBackgroundResource(Resource.Drawable.splash);  
            txtTop.Text = $"{lstRanking[position].Score.ToString(" 0.00 ")}";  
            return view;  
        }  
    }  
}  