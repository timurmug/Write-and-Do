using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandD_nodate.UWP;
using WandD_nodate.CustomElements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Controls;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDPRenderer))]
namespace WandD_nodate.UWP
{
    public class CustomDPRenderer : DatePickerRenderer
    {
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                Control.Margin = new Windows.UI.Xaml.Thickness(0);
                Control.Padding = new Windows.UI.Xaml.Thickness(0);

                SetNativeControl(new Windows.UI.Xaml.Controls.DatePicker { MinWidth = 70 });
            }
        }
    }
}
