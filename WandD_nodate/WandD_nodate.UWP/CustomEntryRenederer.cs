using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandD_nodate.CustomElements;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;
using WandD_nodate.UWP;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenederer))]
namespace WandD_nodate.UWP
{
    public class CustomEntryRenederer:EntryRenderer
    {
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                Control.Margin = new Windows.UI.Xaml.Thickness(0);
                Control.Padding = new Windows.UI.Xaml.Thickness(0);
            }
        }
    }
}
