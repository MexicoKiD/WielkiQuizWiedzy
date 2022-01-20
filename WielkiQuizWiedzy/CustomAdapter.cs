using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace WielkiQuizWiedzy
{
    internal class CustomAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly List<Ranking> lstRanking;

        public CustomAdapter(Context context, List<Ranking> lstRanking)
        {
            this.context = context;
            this.lstRanking = lstRanking;
        }

        public override int Count => lstRanking.Count;

        public override Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.row, null);
            var txtTop = view.FindViewById<TextView>(Resource.Id.txtTop);
            var imageTop = view.FindViewById<ImageView>(Resource.Id.imgTop);
            if (position == 0) imageTop.SetBackgroundResource(Resource.Drawable.icon_gold);
            else if (position == 1) imageTop.SetBackgroundResource(Resource.Drawable.icon_silver);
            else if (position == 2) imageTop.SetBackgroundResource(Resource.Drawable.icon_bronze);
            else imageTop.SetBackgroundResource(Resource.Drawable.ic_logo);
            txtTop.Text = $"{lstRanking[position].Score.ToString(" 0.00 ")}";
            return view;
        }
    }
}