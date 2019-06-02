using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WandD_nodate.Views
{
	public class AddPage : ContentPage
	{
		public AddPage ()
		{
			Content = new StackLayout {
				Children = {
                    new Label
                    {
                        Text = "Добавление новой заметки",
                        HorizontalOptions=LayoutOptions.CenterAndExpand,
                        VerticalOptions=LayoutOptions.CenterAndExpand
                    }
                }
			};
		}
	}
}