using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandD_nodate.CustomElements;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;
using WandD_nodate.UWP;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer_UWP))]
namespace WandD_nodate.UWP
{
    public class CustomButtonRenderer_UWP:ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Padding = new Windows.UI.Xaml.Thickness(0, 0, 0, 0);
            }
        }
    }
}
