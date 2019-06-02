using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System;
using System.ComponentModel;
using WandD_nodate.Views;
using Plugin.Settings;
using System.Threading.Tasks;
using WandD_nodate.Views_UWP;

namespace WandD_nodate.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public static NoteVM oldNote;
        //public static bool scroll=false;
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNoteCommand { protected set; get; }
        //public ICommand CreateTodayNoteCommand { protected set; get; }
        public ICommand DeleteNoteCommand { protected set; get; }
        public ICommand SaveNoteCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }


        public INavigation Navigation { get; set; }

        public NotesViewModel()
        {
            CreateNoteCommand = new Command(CreateNote);
            //CreateTodayNoteCommand = new Command(CreateTodayNote);
            DeleteNoteCommand = new Command(DeleteNote);
            SaveNoteCommand = new Command(SaveNote);
            //SaveNewNoteCommand = new Command(SaveNewNote);
            BackCommand = new Command(Back);
        }

        //метод для отображения выполненных сегодня задач в тулбаре
        public static string /*Task<string>*/ CountNotes()
        {
            switch (App.todaydonenotes)
            {
                case 0: return "count0.png";
                case 1: return "count1.png";
                case 2: return "count2.png";
                case 3: return "count3.png";
                case 4: return "count4.png";
                case 5: return "count5.png";
                case 6: return "count6.png";
                case 7: return "count7.png";
                case 8: return "count8.png";
                case 9: return "count9.png";
                case 10: return "count10.png";
                default: return "count_nothing.png";
            }
        }

        //удаление айтема из listview
        public Command<NoteVM> RemoveCommand
        {
            get => new Command<NoteVM>(async (note) =>
            {
                //await App.Database.DeleteItemAsync(note);
                await App.Database.DoneItemAsync(note);
                oldNote = null;
                App.todaydonenotes++;
                App.alldonenotes++;
                CrossSettings.Current.AddOrUpdateValue("todaydonenotes", App.todaydonenotes);
                CrossSettings.Current.AddOrUpdateValue("alldonenotes", App.alldonenotes);
            });
            set { }
        }

        //public static NoteVM selectednote;
        //раскрытие или скрывание дополнения
        public void HideorShowNotes(NoteVM note)
        {
            //selectednote = note;
            if (oldNote == note)
            {
                //при повторном нажатии скрываются дополнения
                note.IsVisible = !note.IsVisible;
                UpdateNotes(note);
            }
            else
            {
                if (oldNote != null)
                {
                    //спрятать раньше раскрытое дополнение
                    oldNote.IsVisible = false;
                    UpdateNotes(oldNote);
                }
                if (note != null)
                {
                    //раскрыть выбранное дополнение
                    note.IsVisible = true;
                    UpdateNotes(note);

                    //scroll = true;
                }
            }
            oldNote = note;
        }

        //обновление при раскрытии/скрытии дополнения
        public async static void UpdateNotes(NoteVM note)
        {
            await App.database.SaveItemAsync(note);
        }

        //public async void UpdateSubTasks(NoteVM note)
        //{
        //    OnPropertyChanged("UpdateSubTasks");
        //    await App.Database.SaveItemAsync(note);
        //}

        public void SelectedNote(NoteVM note)
        {
            OnPropertyChanged("SelectedNote");
            Navigation.PushAsync(new EditNotePage(note));
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void CreateNote()
        {
            //switch (Device.RuntimePlatform)
            //{
            //    case Device.Android:
            //       await Navigation.PushAsync(new NewNotePage(new NoteVM() { ListViewModel = this, Date = DateTime.Today }));
            //        break;
            //    case Device.UWP:
            //        await Navigation.PushAsync(new NewNotePage_UWP(
            //            /*new NoteVM() { ListViewModel = this, Date = DateTime.Today }*/));
            //        break;
            //    default: break;
            //}
            await Navigation.PushAsync(new NewNotePage(new NoteVM() { ListViewModel = this, Date = DateTime.Today }));
        }

        private async void CreateTodayNote()
        {
            await Navigation.PushAsync(new NewNotePage(new NoteVM() { ListViewModel = this, Date = DateTime.Today }));
        }

        private async void Back()
        {
            await Navigation.PopAsync();
        }

        private async void DeleteNote(object noteObject)
        {
            NoteVM note = noteObject as NoteVM;
            if (note != null)
            {
                await App.Database.DeleteItemAsync(note);
            }
            await Navigation.PopAsync();
            oldNote = null;
        }

        private async void SaveNote(object noteObject)
        {
            NoteVM note = noteObject as NoteVM;
            if (note != null && note.IsValid)
            {
                await App.Database.SaveItemAsync(note);
            }
            Back();
        }
    }
}
