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
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomEditor2), typeof(CustomEditor_Renderer))]
namespace WandD_nodate.Droid
{
    public class CustomEditor_Renderer:EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.SetTextColor(Color.FromHex("0D47A1").ToAndroid());
                this.Control.Gravity = GravityFlags.CenterVertical;
                this.Control.SetPadding(0, 0, 20, 2);

                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);

                var element = e.NewElement as CustomEditor2;
                this.Control.Hint = element.Placeholder;
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomEditor2.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as CustomEditor2;
                this.Control.Hint = element.Placeholder;
            }
        }
    }
}