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
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace WandD_nodate.Droid
{
    public class CustomButtonRenderer:ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            Android.Widget.Button button = Control as Android.Widget.Button;

            if (button != null)
            {
                button.Gravity = GravityFlags.Center;
                button.SetPadding(0, 0, 0, 0);
            }
        }

        //protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        //{
        //    base.OnElementChanged(e);
        //    UpdateAlignment();
        //    UpdateFont();
        //}

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == CustomButton.VerticalContentAlignmentProperty.PropertyName ||
        //        e.PropertyName == CustomButton.HorizontalContentAlignmentProperty.PropertyName)
        //    {
        //        UpdateAlignment();
        //    }
        //    else if (e.PropertyName == Xamarin.Forms.Button.FontProperty.PropertyName)
        //    {
        //        UpdateFont();
        //    }
        //    base.OnElementPropertyChanged(sender, e);
        //}

        //private void UpdateFont()
        //{
        //    Control.Typeface = Element.Font.ToExtendedTypeface(Context);
        //}

        //private void UpdateAlignment()
        //{
        //    var element = this.Element as CustomButton;

        //    if (element == null || this.Control == null)
        //    {
        //        return;
        //    }

        //    this.Control.Gravity = element.VerticalContentAlignment.ToDroidVerticalGravity() |
        //        element.HorizontalContentAlignment.ToDroidHorizontalGravity();
        //}
    }
}