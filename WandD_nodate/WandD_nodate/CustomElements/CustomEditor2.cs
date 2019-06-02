using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WandD_nodate.CustomElements
{
    public class CustomEditor2 : Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create<CustomEditor2, string>(view => view.Placeholder, String.Empty);

        public CustomEditor2()
        {
            //if (Device.RuntimePlatform==Device.UWP)
            //        InvalidateMeasure();
            TextChanged += OnTextChanged;
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }


        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            InvalidateMeasure();
        }
    }
}
