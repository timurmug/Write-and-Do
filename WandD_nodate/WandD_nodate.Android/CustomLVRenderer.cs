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
using WandD_nodate.Droid;
using WandD_nodate.CustomElements;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomLVRenderer))]
namespace WandD_nodate.Droid
{
    public class CustomLVRenderer:ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.VerticalScrollBarEnabled = false;
                Control.HorizontalScrollBarEnabled = false;
            }
        }
    }
}