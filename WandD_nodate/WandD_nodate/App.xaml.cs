using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WandD_nodate.ViewModels;
using System.Threading.Tasks;
using Plugin.Settings;
using System.Threading;
using WandD_nodate.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace WandD_nodate
{
	public partial class App : Application
	{
        public static string todayDateString = CrossSettings.Current.GetValueOrDefault("todayDateString", DateTime.Today.ToString("dd.MM.yyyy"));
        public static int todaydonenotes = CrossSettings.Current.GetValueOrDefault("todaydonenotes", 0);
        public static int alldonenotes = CrossSettings.Current.GetValueOrDefault("alldonenotes", 0);
        public static bool showoverdue1 = CrossSettings.Current.GetValueOrDefault("showoverdue1", true);
        public static bool showoverdue2 = CrossSettings.Current.GetValueOrDefault("showoverdue2", true);
        //public static bool darktheme = CrossSettings.Current.GetValueOrDefault("darktheme", false);
        public static bool pastaction = false;

        public static MainPage mainPage;
        public const string DATABASE_NAME = "notes.db";
        public static NoteRepository database;
        public static NoteRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteRepository(DATABASE_NAME);
                }
                return database;
            }
        }

        public App ()
		{
            InitializeComponent();
            //MainPage = mainPage;
            
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    MainPage =new MainPage();
                    //MainPage = new NavigationPage(new MainPage());
                    //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("1564c0");
                    break;
                //case Device.iOS: appLabel.Text = "iOS";break;
                case Device.UWP:
                    MainPage = new MainPage_UWP();
                    //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("1564c0");
                    break;
                default: break;
            }
        }

		protected override void OnStart ()
		{
            Update();
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    TodayPage.nonotesImage.Source = Addition.ChooseImage();
                    allNotesPage.nonotesImage.Source = Addition.ChooseImage();
                    break;
                case Device.UWP:
                    MainPage_UWP.nodateImage.Source = "images/" + Addition.ChooseImage();
                    break;
                default: break;
            }

            //TodayPage.nonotesImage.Source = Addition.ChooseImage();
            //allNotesPage.nonotesImage.Source = Addition.ChooseImage();
        }

		protected override void OnSleep ()
		{
            //закрытие раскрытой €чейки
            if (NotesViewModel.oldNote != null)
            {
                NotesViewModel.oldNote.IsVisible = false;
                NotesViewModel.UpdateNotes(NotesViewModel.oldNote);
            }
            CrossSettings.Current.AddOrUpdateValue("todaydonenotes", todaydonenotes);
            CrossSettings.Current.AddOrUpdateValue("alldonenotes", alldonenotes);
            CrossSettings.Current.AddOrUpdateValue("todayDateString", todayDateString);
            CrossSettings.Current.AddOrUpdateValue("showoverdue1", showoverdue1);
            CrossSettings.Current.AddOrUpdateValue("showoverdue2", showoverdue2);
        }

		protected override void OnResume ()
        {
            Update();
        }

        private async void Update()
        {
            await Database.Update();
            if (NotesViewModel.oldNote != null)
            {
                NotesViewModel.oldNote.IsVisible = false;
                NotesViewModel.UpdateNotes(NotesViewModel.oldNote);
            }
            if (todayDateString != DateTime.Today.ToString("dd.MM.yyyy"))
            {
                todayDateString = DateTime.Today.ToString("dd.MM.yyyy");
                CrossSettings.Current.AddOrUpdateValue("todayDate", DateTime.Today.ToString("dd.MM.yyyy"));
                todaydonenotes = 0;
                CrossSettings.Current.AddOrUpdateValue("todaydonenotes", todaydonenotes);
            }
            //if (pastaction == true)
            //{
            //    Thread myThread = new Thread(new ThreadStart(() =>
            //    {
            //        Thread.Sleep(1000);
            //        pastaction = false;
            //    }));
            //    myThread.Start(); // запускаем поток
            //}

            //if (darktheme == true)
            //{
            //    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("1e1e1e");
            //}
            //else
            //{
            //    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("1564c0");
            //}
        }
    }
}
