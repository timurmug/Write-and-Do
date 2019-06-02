using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BottomBar.XamarinForms;
using WandD_nodate.Views;


namespace WandD_nodate
{
	public partial class MainPage : BottomBarPage
    {
		public MainPage()
		{
            InitializeComponent();
            //var pages = this.Children.GetEnumerator();
            //pages.MoveNext(); // First page
            //pages.MoveNext(); // Second page
            //CurrentPage = pages.Current;
            //this.CurrentPage = this.Children[1];

            //    var secondPage = new TodayPage { Title = "Сегодня", Icon = "today.png" };
            //    BottomBarPage bottomBar = new BottomBarPage { FixedMode = false };
            //    NavigationPage.SetHasNavigationBar(bottomBar, false);

            //    bottomBar.Children.Add(new allNotesPage());
            //    bottomBar.Children.Add(new TodayPage { Title = "Сегодня" });
            //    Children.Add(new allNotesPage());
            //    Children.Add(secondPage);

            //    var pages = this.Children.GetEnumerator();
            //    pages.MoveNext(); // First page
            //    pages.MoveNext(); // Second page

            //    bottomBar.CurrentPage = pages.Current;
            //    Application.Current.MainPage = new NavigationPage(bottomBar);
        }
    }
}
