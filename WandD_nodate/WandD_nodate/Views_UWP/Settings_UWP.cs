using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WandD_nodate.Views_UWP
{
	public class Settings_UWP : ContentPage
	{
        ///////////статистика///////////////////
        public static Label statisticsLabel = new Label
        {
            TextColor = Color.FromHex("0D47A1"),
            Text = "Статистика",
            FontFamily = "Open Sans",
            Margin = new Thickness(0, 15, 0, 0),
            FontSize = 12.5
        };
        public static Label todaynotesLabel = new Label
        {
            FontFamily = "Open Sans",
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
        };
        public static Label expiredLabel = new Label
        {
            FontFamily = "Open Sans",
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
        };
        public static Label allnotesLabel = new Label
        {
            FontFamily = "Open Sans",
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
        };
        public static Label doneLabel = new Label
        {
            FontFamily = "Open Sans",
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
        };
        public static StackLayout statisticsSL = new StackLayout
        {
            Padding = new Thickness(5, 10, 0, 0),
            Children = { todaynotesLabel, doneLabel, expiredLabel, allnotesLabel }
        };       

        ///////////настройки///////////////////
        public static Label settingsLabel = new Label
        {
            FontFamily = "Open Sans",
            TextColor = Color.FromHex("0D47A1"),
            Text = "Настройки",
            Margin = new Thickness(0, 15, 0, 0),
            FontSize = 12.5
        };

        public static Label showoverdueLabel_today = new Label
        {
            FontFamily = "Open Sans",
            Text = "Просроченные заметки\nна вкладке \"Cегодня\"",
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
        };
        public static Switch showoverdueSwitch_today = new Switch
        {
            WidthRequest = 45,
            IsToggled = App.showoverdue2,
            HorizontalOptions = LayoutOptions.EndAndExpand,
            Margin = new Thickness(0, 5, 0, 0)
        };
        public static Label showoverdueLabel_all = new Label
        {
            FontFamily = "Open Sans",
            Text = "Просроченные заметки\nна вкладке \"Все\"",
            FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
        };
        public static Switch showoverdueSwitch_all = new Switch
        {
            WidthRequest = 45,
            IsToggled = App.showoverdue1,
            HorizontalOptions = LayoutOptions.EndAndExpand,
            Margin=new Thickness(0,5,0,0)
        };
        
        public static StackLayout settingsSL = new StackLayout
        {
            Padding = new Thickness(5, 10, 0, 0),
            Children =
                {
                    new StackLayout
                    {
                        Children ={showoverdueLabel_today,showoverdueSwitch_today},
                        Orientation=StackOrientation.Horizontal
                    },
                    new StackLayout
                    {
                        Children ={showoverdueLabel_all,showoverdueSwitch_all},
                        Orientation=StackOrientation.Horizontal
                    }
                }
        };

        ///////////расположение элементов
        public static StackLayout otherSL = new StackLayout
        {
            Children = { statisticsLabel, statisticsSL, settingsLabel, settingsSL},
            IsVisible = true
        };

        //public Settings_UWP ()
        //{
        //}

        public static void ShowoverdueSwitchALL_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
                App.showoverdue1 = true;
            else
                App.showoverdue1 = false;
            CrossSettings.Current.AddOrUpdateValue("showoverdue1", App.showoverdue1);
            //MainPage_UWP.Refresh();
        }

        public static void ShowoverdueSwitchToday_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
                App.showoverdue2 = true;
            else
                App.showoverdue2 = false;
            CrossSettings.Current.AddOrUpdateValue("showoverdue2", App.showoverdue2);
            //MainPage_UWP.Refresh();
        }
    }
}