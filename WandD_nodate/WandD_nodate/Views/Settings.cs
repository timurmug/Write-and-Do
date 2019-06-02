using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WandD_nodate.ViewModels;
using BottomBar.XamarinForms;

using Xamarin.Forms;

namespace WandD_nodate.Views
{
    public class Settings : ContentPage
	{
        Label todaynotesLabel = new Label
        {
            //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        };
        Label expiredLabel = new Label
        {
            //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        };
        Label allnotesLabel = new Label
        {
            //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        };
        Label doneLabel = new Label
        {
            //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        };
        StackLayout contentSL;
        Switch darkthemeSwitch;
        Label darkthemeLabel;
        Label statisticsLabel;
        Label settingsLabel;
        Label showoverdueLabel1;
        Label showoverdueLabel2;
        Button backButton;

        public Settings ()
		{
            //((NavigationPage)Application.Current.MainPage).Title = "Профиль";
            //App.Current.MainPage.Title = "Профиль";
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            Title = "Профиль";
            NavigationPage.SetHasBackButton(this, false);
            //NavigationPage.SetHasNavigationBar(this, false);

            ///////////статистика//////////////////////
            statisticsLabel = new Label
            {
                //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
                TextColor = Color.FromHex("0D47A1"),
                Text = "Статистика",
                Margin = new Thickness(5, 15, 0, 0),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            StackLayout statisticsSL = new StackLayout
            {
                Padding = new Thickness(15, 10, 0, 0),
                Children = {todaynotesLabel, doneLabel, expiredLabel, allnotesLabel }
            };


            ///////////настройки//////////////////////
            settingsLabel = new Label
            {
                //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
                TextColor = Color.FromHex("0D47A1"),
                Text = "Настройки",
                Margin = new Thickness(5, 15, 0, 0),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };

            showoverdueLabel1 = new Label
            {
                Text = "Просроченные заметки на вкладке \"Все\"",
                //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            Switch showoverdueSwitch1 = new Switch { IsToggled = App.showoverdue1, HorizontalOptions = LayoutOptions.EndAndExpand };
            showoverdueSwitch1.Toggled += ShowoverdueSwitch1_Toggled;

            showoverdueLabel2 = new Label
            {
                Text = "Просроченные заметки на вкладке \"Cегодня\"",
                //FontFamily = Device.RuntimePlatform == Device.Android ? "URWGeometric-Regular.otf#URW Geometric" : null,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            Switch showoverdueSwitch2 = new Switch { IsToggled=App.showoverdue2, HorizontalOptions = LayoutOptions.EndAndExpand };
            showoverdueSwitch2.Toggled += ShowoverdueSwitch2_Toggled;

            //darkthemeLabel = new Label
            //{
            //    Text = "Тёмная тема",
            //    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            //};
            //darkthemeSwitch = new Switch
            //{
            //    HorizontalOptions = LayoutOptions.EndAndExpand,
            //    IsToggled = App.darktheme
            //};
            //darkthemeSwitch.Toggled += DarkthemeSwitch_Toggled;

            StackLayout settingsSL = new StackLayout
            {
                Padding = new Thickness(15, 10, 0, 0),
                Children =
                {
                    //new StackLayout
                    //{
                    //    Children ={darkthemeLabel,darkthemeSwitch},
                    //    Orientation=StackOrientation.Horizontal
                    //},
                    new StackLayout
                    {
                        Children ={showoverdueLabel2,showoverdueSwitch2},
                        Orientation=StackOrientation.Horizontal
                    },
                    new StackLayout
                    {
                        Children ={showoverdueLabel1,showoverdueSwitch1},
                        Orientation=StackOrientation.Horizontal
                    }
                }
            };

            backButton = new Button
            {
                Text = "➥",
                BackgroundColor=Color.White,
                TextColor = Color.FromHex("0D47A1"),
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            backButton.Clicked += BackButton_Clicked;

            contentSL= new StackLayout
            {
                Children =
                {
                    statisticsLabel,
                    statisticsSL,
                    settingsLabel,
                    settingsSL,
                    backButton
                },
                BackgroundColor=Color.White,
                Padding = new Thickness(15)
            };
            //UpdateColors();
            Content = contentSL;
		}

        //void UpdateColors()
        //{
        //    if (App.darktheme == true)
        //    {
        //        contentSL.BackgroundColor = Color.FromHex("#3e3e42");

        //        todaynotesLabel.TextColor = Color.White;
        //        doneLabel.TextColor = Color.White;
        //        expiredLabel.TextColor = Color.White;
        //        allnotesLabel.TextColor = Color.White;

        //        statisticsLabel.TextColor = Color.Gray;
        //        settingsLabel.TextColor = Color.Gray;
        //        showoverdueLabel1.TextColor = Color.White;
        //        showoverdueLabel2.TextColor = Color.White;
        //        darkthemeLabel.TextColor = Color.White;

        //        backButton.BackgroundColor = Color.FromHex("#3e3e42");
                
        //        ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("1e1e1e");
        //    }
        //    else
        //    {
        //        contentSL.BackgroundColor = Color.White;

        //        todaynotesLabel.TextColor = Color.Gray;
        //        doneLabel.TextColor = Color.Gray;
        //        expiredLabel.TextColor = Color.Gray;
        //        allnotesLabel.TextColor = Color.Gray;

        //        statisticsLabel.TextColor = Color.FromHex("0D47A1");
        //        settingsLabel.TextColor = Color.FromHex("0D47A1");
        //        showoverdueLabel1.TextColor = Color.Gray;
        //        showoverdueLabel2.TextColor = Color.Gray;
        //        darkthemeLabel.TextColor = Color.Gray;

        //        backButton.BackgroundColor = Color.White;
                
        //        ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("1564c0");
        //    }
        //}

        //private void DarkthemeSwitch_Toggled(object sender, ToggledEventArgs e)
        //{
        //    if (e.Value == true)
        //    {
        //        App.darktheme = true;
        //        UpdateColors();
        //    }
        //    else
        //    {
        //        App.darktheme = false;
        //        UpdateColors();
        //    }
        //    CrossSettings.Current.AddOrUpdateValue("darktheme", App.darktheme);
        //}

        private void ShowoverdueSwitch1_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
                App.showoverdue1 = true;
            else
                App.showoverdue1 = false;
            CrossSettings.Current.AddOrUpdateValue("showoverdue1", App.showoverdue1);
        }

        private void ShowoverdueSwitch2_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == true)
                App.showoverdue2 = true;
            else
                App.showoverdue2 = false;
            CrossSettings.Current.AddOrUpdateValue("showoverdue2", App.showoverdue2);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //darkthemeSwitch.IsToggled = App.darktheme;
            //UpdateColors();
            todaynotesLabel.Text = "Сегодня выполнено задач: " + App.todaydonenotes;
            doneLabel.Text = "Выполнено за все время: " + App.alldonenotes;
            expiredLabel.Text = "Просрочено: " + await App.Database.CountExpiredItems();
            allnotesLabel.Text="Запланировано: "+ await App.Database.CountItems();
           
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}