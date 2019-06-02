using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SQLite;
using WandD_nodate.Models;
using Xamarin.Forms;

namespace WandD_nodate.ViewModels
{
    [Table("Notes")]
    public class NoteVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        NotesViewModel lvm;

        public Note Note { get; private set; }

        public NoteVM()
        {
            Note = new Note();
        }

        [Ignore]
        public NotesViewModel ListViewModel
        {
            get { return lvm; }
            set
            {
                if (lvm != value)
                {
                    lvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return Note.Id; }
            set
            {
                if (Note.Id != value)
                {
                    Note.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get { return Note.Name; }
            set
            {
                if (Note.Name != value)
                {
                    Note.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Comment
        {
            get { return Note.Comment; }
            set
            {
                if (Note.Comment != value)
                {
                    Note.Comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        [Ignore]
        public bool HasComment
        {
            get
            {
                if (String.IsNullOrEmpty(Comment) == false)
                    return true;
                else
                    return false;
            }
        }

        public DateTime Date
        {
            get
            {
                return Note.date_Datatime;
            }
            set
            {
                if (Note.date_Datatime != value)
                {
                    Note.date_Datatime = value;

                    if (Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.ToString("dd.MM.yyyy"))
                        Date_str = Note.date_Datatime.ToString("Сегодня, d MMMM");
                    else if (Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.AddDays(+1).ToString("dd.MM.yyyy"))
                        Date_str = Note.date_Datatime.ToString("Завтра, d MMMM");
                    else if (Note.date_Datatime < DateTime.Today)
                        Date_str = "Просрочено";
                    else if (Note.date_Datatime.Year > DateTime.Today.Year)
                        Date_str = Note.date_Datatime.ToString("d MMMM yyyy");
                    else if (Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.AddDays(+2).ToString("dd.MM.yyyy"))
                        Date_str = Note.date_Datatime.ToString("Послезавтра, dd MMMM");
                    else if (Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.AddDays(+3).ToString("dd.MM.yyyy") ||
                             Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.AddDays(+4).ToString("dd.MM.yyyy") ||
                             Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.AddDays(+5).ToString("dd.MM.yyyy") ||
                             Note.date_Datatime.ToString("dd.MM.yyyy") == DateTime.Today.AddDays(+6).ToString("dd.MM.yyyy"))
                    {
                        string str = Note.date_Datatime.ToString("dddd, dd MMMM");
                        Date_str = str.Substring(0, 1).ToUpper() + str.Remove(0, 1);
                    }
                    else
                        Date_str = Note.date_Datatime.ToString("d MMMM");
                    
                    OnPropertyChanged("Date");
                }
            }
        }

        public string Date_str
        {
            get
            {
                return Note.date_string;
            }
            set
            {
                Note.date_string = value;
                OnPropertyChanged("Date_str");
            }
        }

        public bool IsVisible
        {
            get { return Note.IsVisible; }
            set
            {
                if (Note.IsVisible != value)
                {
                    Note.IsVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name.Trim())) ||
                    (!string.IsNullOrEmpty(Comment.Trim())));
            }
        }

        public bool IsOverdue
        {
            get { return Note.isOverdue; }
            set
            {
                if (Note.isOverdue != value)
                {
                    Note.isOverdue = value;
                    Overdue_str = "(от " + Note.date_Datatime.ToString("dd MMMM") + ")";
                    OnPropertyChanged("IsOverdue");
                }
            }
        }

        public string Overdue_str
        {
            get
            {
                return Note.Overdue_str;
            }
            set
            {
                Note.Overdue_str = value;
                OnPropertyChanged("Overdue_str");
            }
        }

        public bool Repeat
        {
            get
            {
                return Note.Repeat;
            }
            set
            {
                Note.Repeat = value;
                OnPropertyChanged("Repeat");
            }
        }

        public bool everyMonday
        {
            get
            {
                return Note.everyMonday;
            }
            set
            {
                Note.everyMonday = value;
                OnPropertyChanged("everyMonday");
            }
        }
        public bool everyTuesday
        {
            get
            {
                return Note.everyTuesday;
            }
            set
            {
                Note.everyTuesday = value;
                OnPropertyChanged("everyTuesday");
            }
        }
        public bool everyWednesday
        {
            get
            {
                return Note.everyWednesday;
            }
            set
            {
                Note.everyWednesday = value;
                OnPropertyChanged("everyWednesday");
            }
        }
        public bool everyThursday
        {
            get
            {
                return Note.everyThursday;
            }
            set
            {
                Note.everyThursday = value;
                OnPropertyChanged("everyThursday");
            }
        }
        public bool everyFriday
        {
            get
            {
                return Note.everyFriday;
            }
            set
            {
                Note.everyFriday = value;
                OnPropertyChanged("everyFriday");
            }
        }
        public bool everySaturday
        {
            get
            {
                return Note.everySaturday;
            }
            set
            {
                Note.everySaturday = value;
                OnPropertyChanged("everySaturday");
            }
        }
        public bool everySunday
        {
            get
            {
                return Note.everySunday;
            }
            set
            {
                Note.everySunday = value;
                OnPropertyChanged("everySunday");
            }
        }
        
        public string ColorMarker_string
        {
            get
            {
                return Note.ColorMarker_string;
            }
            set
            {
                Note.ColorMarker_string = value;
                OnPropertyChanged("ColorMarker_string");
            }
        }

        [Ignore]
        public double ButtonsWidth
        {
            get
            {
                if (Color.White == Color.FromHex(Note.ColorMarker_string))
                    return 1;
                else
                    return 0;
            }
        }

        [Ignore]
        public Color ColorMarker
        {
            get
            {
                if (Color.White==Color.FromHex(Note.ColorMarker_string))
                    return Color.Transparent;
                else
                    return Color.FromHex(Note.ColorMarker_string);
            }
        }

        [Ignore]
        public Color ColorSubtasks
        {
            get
            {
                if (Color.White == Color.FromHex(Note.ColorMarker_string))
                    return Color.LightGray;
                else
                    return Color.FromHex(Note.ColorMarker_string);
            }
        }
        
        [Ignore]
        public Color TextColorSubtasks
        {
            get
            {
                if (Color.White == Color.FromHex(Note.ColorMarker_string))
                    return Color.Gray;
                else
                    return Color.FromHex(Note.ColorMarker_string);
            }
        }

        public string Subtasks_string
        {
            get
            {
                return Note.subtasks_string;
            }
            set
            {
                Note.subtasks_string = value;
                OnPropertyChanged("Subtasks_string");
            }
        }

        [Ignore]
        public bool HasSubtasks
        {
            get
            {
                //if (Subtasks_string != String.Empty)
                if ((String.IsNullOrEmpty(Subtasks_string) == false))
                    return true;
                else
                    return false;
            }
        }

        [Ignore]
        public int CountSubtasks
        {
            get
            {
                int count_subtasks=0;
                foreach (char s in Subtasks_string)
                {
                    if (s == '✖')
                        count_subtasks++;
                }
                return count_subtasks;
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
