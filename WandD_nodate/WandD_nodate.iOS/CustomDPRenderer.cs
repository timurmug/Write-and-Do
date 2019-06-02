using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class CustomDPRenderer: DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            Control.BorderStyle = UITextBorderStyle.RoundedRect;
            Control.Layer.BorderColor = Color.FromHex("0D47A1").ToCGColor();  
            Control.Layer.CornerRadius = 10;
            Control.Layer.BorderWidth = 1f;
            Control.TextAlignment = UITextAlignment.Center;
            Control.AdjustsFontSizeToFitWidth = true;
            Control.TextColor = Color.FromHex("0D47A1").ToUIColor();   

            //Control.Layer.BorderWidth = 0;
            //Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}