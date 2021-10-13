using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project5WorkLog
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class MonthActiv : ContentPage
    {
        SQLiteConnection conn;
        private string year;
        private string v;

        public MonthActiv(string year, string v)
        {
            InitializeComponent();
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "WorkLog.db");
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Job>();
            List<Job> filteredList = new List<Job>();
            foreach (Job j in conn.Query<Job>("select * from job order by date"))
            {
                string monthModded = (j.date.Month + "").Length == 1 ? "0" + j.date.Month : j.date.Month + "";
                if (monthModded == v && j.date.Year + "" == year)
                {
                    filteredList.Add(j);
                }
            }
            totalList.ItemsSource = filteredList;
        }

        //public MonthActiv(string year, string v)
        //{
        //    this.year = year;
        //    this.v = v;
        //    InitializeComponent();
        //    string libFolder = FileSystem.AppDataDirectory;
        //    string fname = System.IO.Path.Combine(libFolder, "WorkLog.db");
        //    conn = new SQLiteConnection(fname);
        //    conn.CreateTable<Job>();
        //    List<Job> filteredList = new List<Job>();
        //    foreach (Job j in conn.Table<Job>().ToList())
        //    {
        //        if (j.date.Month + "" == v && j.date.Year + "" == year)
        //        {
        //            filteredList.Add(j);
        //        }
        //    }
        //    totalList.ItemsSource = filteredList;
        //}

        private void totalList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
    }
}