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

[assembly: ExportRenderer(typeof(CustomEditor2), typeof(CustomEditorRenderer))]
namespace WandD_nodate.UWP
{
    public class CustomEditorRenderer:EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
                Control.Margin = new Windows.UI.Xaml.Thickness(0);
                Control.Padding = new Windows.UI.Xaml.Thickness(0,5,0,0);
                Control.VerticalContentAlignment = new Windows.UI.Xaml.VerticalAlignment();
                Control.VerticalAlignment = new Windows.UI.Xaml.VerticalAlignment();

                var element = e.NewElement as CustomEditor2;
                this.Control.PlaceholderText = element.Placeholder;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomEditor2.PlaceholderProperty.PropertyName)
            {
                var element = this.Element as CustomEditor2;
                this.Control.PlaceholderText = element.Placeholder;
            }
        }


    }
}
