using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using WandD_nodate.Models;
using System.Threading.Tasks;
using System;

namespace WandD_nodate.ViewModels
{
    public class NoteRepository
    {
        //SQLiteConnection database;
        SQLiteAsyncConnection database;
        static object locker = new object();

        public NoteRepository(string filename)
        {
            string databasePath = DependencyService.Get<ISQLite>().GetDatabasePath(filename);
            //database = new SQLiteConnection(databasePath);
            database = new SQLiteAsyncConnection(databasePath);
            //database.CreateTable<NoteVM>();
            database.CreateTableAsync<NoteVM>();
        }
        
        //создание таблицы
        public async Task CreateTable()
        {
            await database.CreateTableAsync<NoteVM>();
        }

        //получение всех заметок С/Без просроченных
        public async Task<List<NoteVM>> GetItemsAsync()
        {
            if (App.showoverdue1==true)
                return await database.Table<NoteVM>().ToListAsync();
            else
            {
                List<NoteVM> Notes = new List<NoteVM>();
                foreach (var n in await database.Table<NoteVM>().ToListAsync())
                {
                    if (n.Date >= System.DateTime.Today)
                        Notes.Add(n);
                }
                return Notes;
            }
        }

        //подсчет всех заметок (кроме просроченных)
        public async Task<int> CountItems()
        {
            //return await database.Table<NoteVM>().CountAsync();
            List<NoteVM> Notes = new List<NoteVM>();
            foreach (var n in await database.Table<NoteVM>().ToListAsync())
            {
                if (n.Date >= System.DateTime.Today)
                    Notes.Add(n);
            }
            return Notes.Count;
        }

        //подсчет просроченных заметок
        public async Task<string> CountExpiredItems()
        {
            List<NoteVM> expiredNotes = new List<NoteVM>();
            foreach (var n in await database.Table<NoteVM>().ToListAsync())
            {
                if (n.Date < System.DateTime.Today)
                    expiredNotes.Add(n);
            }
            return expiredNotes.Count.ToString();
        }

        //
        public async Task<int> CountTodayItems()
        {
            List<NoteVM> todayNotes = new List<NoteVM>();
            foreach (var n in await database.Table<NoteVM>().ToListAsync())
            {
                if (n.Date <= System.DateTime.Today)
                    todayNotes.Add(n);
            }
            return todayNotes.Count;
        }

        //получение заметок на сегодня С/Без просроченных
        public async Task<List<NoteVM>> TodayNotesAsync()
        {
            List<NoteVM> todayNotes=new List<NoteVM>();
            foreach (var n in await database.Table<NoteVM>().ToListAsync())
            {
                if (App.showoverdue2==true)
                {
                    if (n.Date <= System.DateTime.Today)
                        todayNotes.Add(n);
                }
                else
                {
                    if (n.Date == System.DateTime.Today)
                        todayNotes.Add(n);
                }
            }
            return todayNotes;
        }

        //получение айтема по id
        public async Task<NoteVM> GetItemAsync(int id)
        {
            return await database.GetAsync<NoteVM>(id);
        }

        //выполнение заметки
        public async Task<int> DoneItemAsync(NoteVM item)
        {
            //switch (DateTime.Today.AddDays(+1).DayOfWeek)
            //{
            //    case (DayOfWeek.Monday):
            //        if (item.everyMonday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //    case (DayOfWeek.Tuesday):
            //        if (item.everyTuesday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //    case (DayOfWeek.Wednesday):
            //        if (item.everyWednesday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //    case (DayOfWeek.Thursday):
            //        if (item.everyWednesday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //    case (DayOfWeek.Friday):
            //        if (item.everyFriday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //    case (DayOfWeek.Saturday):
            //        if (item.everySaturday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //    case (DayOfWeek.Sunday):
            //        if (item.everySunday == true)
            //        {
            //            await database.DeleteAsync(item);
            //            if (item.Date < DateTime.Today)
            //                item.Date = DateTime.Today.AddDays(+1);
            //            else
            //                item.Date = item.Date.AddDays(+1);
            //            item.IsVisible = false;
            //            return await database.InsertAsync(item);
            //        }
            //        break;
            //}
            if (item.Repeat == true)
            {
                await database.DeleteAsync(item);
                if (item.Date < DateTime.Today)
                    item.Date = DateTime.Today.AddDays(+1);
                else
                    item.Date = item.Date.AddDays(+1);
                item.IsVisible = false;
                return await database.InsertAsync(item);
            }
            else
                return await database.DeleteAsync(item);
        }

        //удаление
        public async Task<int> DeleteItemAsync(NoteVM item)
        {
            return await database.DeleteAsync(item);
        }

        //сохранение или обновление заметки
        public async Task<int> SaveItemAsync(NoteVM item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        //обновление айтемов
        public async Task Update()
        {
            foreach (var n in await database.Table<NoteVM>().ToListAsync())
            {
                if (n.Date < DateTime.Today)
                {
                    n.IsOverdue = true;
                    n.Date_str = "Просрочено";
                }
                else
                {
                    n.IsOverdue = false;
                }
                if (n.Date == DateTime.Today)
                    n.Date_str = n.Date.ToString("Сегодня, d MMMM");
                if (n.Date == DateTime.Today.AddDays(+1))
                    n.Date_str = n.Date.ToString("Завтра, d MMMM");
                if (n.Date.Year > DateTime.Today.Year)
                    n.Date_str = n.Date.ToString("d MMMM yyyy");
                if (n.Date == DateTime.Today.AddDays(+2))
                    n.Date_str = n.Date.ToString("Послезавтра, dd MMMM");
                if (n.Date == DateTime.Today.AddDays(+3) ||
                            n.Date == DateTime.Today.AddDays(+4) ||
                             n.Date == DateTime.Today.AddDays(+5) ||
                            n.Date == DateTime.Today.AddDays(+6))
                {
                    string str = n.Date.ToString("dddd, dd MMMM");
                    n.Date_str = str.Substring(0, 1).ToUpper() + str.Remove(0, 1);
                }
                if ((n.Date != DateTime.Today)
                    &&(n.Date != DateTime.Today.AddDays(+1))
                    && (n.Date > DateTime.Today)
                    && (n.Date.Year < DateTime.Today.Year)
                    && n.Date != DateTime.Today.AddDays(+2)
                    && n.Date != DateTime.Today.AddDays(+3)
                    && n.Date != DateTime.Today.AddDays(+4)
                    && n.Date != DateTime.Today.AddDays(+5)
                    && n.Date != DateTime.Today.AddDays(+6))
                    n.Date_str = n.Date.ToString("d MMMM");
                await database.UpdateAsync(n);
            }
        }
    }
}
