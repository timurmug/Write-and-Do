using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WandD_nodate;
using WandD_nodate.Droid;
using WandD_nodate.CustomElements;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDPRenderer))]
namespace WandD_nodate.Droid
{
    public class CustomDPRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            this.Control.SetTextColor(Color.FromHex("0D47A1").ToAndroid());
            //this.Control.SetBackgroundColor(Android.Graphics.Color.Black);
            //this.Control.Gravity = GravityFlags.Start;
            this.Control.SetPadding(5, 0, 5, 0);
            //this.Control.TextAlignment = TextAlignmentTextStart;

            GradientDrawable gd = new GradientDrawable();
            //gd.SetCornerRadius(0); //increase or decrease to changes the corner look
            //gd.SetColor(Android.Graphics.Color.Transparent);
            gd.SetStroke(0, Color.FromHex("0D47A1").ToAndroid());

            this.Control.SetBackgroundDrawable(gd);

            //GradientDrawable gd = new GradientDrawable();
            //gd.SetCornerRadius(25); //increase or decrease to changes the corner look
            //gd.SetColor(Android.Graphics.Color.Transparent);
            //gd.SetStroke(3, Color.FromHex("0D47A1").ToAndroid());

            //this.Control.SetBackgroundDrawable(gd);
        }

    }
}