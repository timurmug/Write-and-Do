using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using WandD_nodate;
using WandD_nodate.CustomElements;
using WandD_nodate.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDPRenderer))]
namespace WandD_nodate.iOS
{
    public class EntryRenderer_IOS:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.RoundedRect;
            Control.Layer.BorderColor = Color.FromHex("0D47A1").ToCGColor();
            Control.Layer.CornerRadius = 10;
            Control.Layer.BorderWidth = 1f;
            Control.TextAlignment = UITextAlignment.Center;
            Control.AdjustsFontSizeToFitWidth = true;
            Control.TextColor = Color.FromHex("0D47A1").ToUIColor();
        }
    }
}