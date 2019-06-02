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
using Android.Content.Res;
using Android.Text;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(EntryRenderer_Droid))]
namespace WandD_nodate.Droid
{
    public class EntryRenderer_Droid: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            //if (Control != null)
            //{
            //    this.Control.SetTextColor(Color.FromHex("0D47A1").ToAndroid());
            //    //this.Control.SetBackgroundColor(Android.Graphics.Color.Black);
            //    this.Control.Gravity = GravityFlags.CenterVertical;
            //    this.Control.SetPadding(20, 0, 20, 2);

            //    GradientDrawable gd = new GradientDrawable();
            //    gd.SetCornerRadius(25); //increase or decrease to changes the corner look
            //    gd.SetColor(Android.Graphics.Color.Transparent);
            //    gd.SetStroke(3, Color.FromHex("0D47A1").ToAndroid());

            //    this.Control.SetBackgroundDrawable(gd);
            //}
            if (Control != null)
            {
                this.Control.SetTextColor(Color.FromHex("0D47A1").ToAndroid());
                this.Control.Gravity = GravityFlags.CenterVertical;
                this.Control.SetPadding(0, 0, 20, 2);

                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}