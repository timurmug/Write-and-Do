using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;

namespace WandD_nodate.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Comment { get; set; }
        public bool IsVisible { get; set; } = false;

        public bool isOverdue { get; set; } = false;
        public string Overdue_str { get; set; }

        public string date_string { get; set; }
        public DateTime date_Datatime { get; set; }

        public bool Repeat = false;
        public bool everyMonday = false;
        public bool everyTuesday = false;
        public bool everyWednesday = false;
        public bool everyThursday = false;
        public bool everyFriday = false;
        public bool everySaturday = false;
        public bool everySunday = false;
        
        public string ColorMarker_string="ffffff";

        public string subtasks_string;
    }
}
