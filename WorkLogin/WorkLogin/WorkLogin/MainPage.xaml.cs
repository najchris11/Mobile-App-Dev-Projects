using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using SQLite;

namespace WorkLogin
{
    public partial class MainPage : TabbedPage
    {
        SQLiteConnection conn;
        public Dictionary<DateTime, HoursWorked> hoursWrk = new Dictionary<DateTime, HoursWorked>();

        public MainPage()
        {
            InitializeComponent();
            if (!Preferences.ContainsKey("Sort")) Preferences.Set("Sort", 0);
            addLists();

            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "WorkLog.db");
            string taskQuery = "from item in conn group item by ";
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Job>();
            activitiesList.ItemsSource = conn.Table<Job>().ToList();
            var query = (from job in conn.Table<Job>()
                         group job by job.date);
            //List(query);
            totalList.ItemsSource = hoursWrk.Values;
        }
        //public void List<T>(IEnumerable<T> query)
        //{
        //    new IEnumerable<IGrouping<DateTime, HoursWorked>>();
        //    foreach (T item in query)
        //    {

        //        if (hoursWrk.ContainsKey(item as IGrouping<DateTime, HoursWorked>))
        //        {
        //            hoursWrk.TryGetValue(item.date) {
        //                hours += item.hours;
        //                mins += item.mins;
        //            }
        //        }
        //        else
        //        {
        //            hoursWrk.Add(item.date, new HoursWorked() { date = item.date, hours = item.hours, mins = item.mins });
        //        }
        //    }

        //}


        private void addLists()
        {
            List<int> hours = new List<int>();
            List<int> mins = new List<int>();
            List<string> settings = new List<string>();

            for(int i = 0; i <= 24; i++)
            {
                hours.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                mins.Add(i);
            }
            settings.Add("by Date");
            settings.Add("by Job");
            hoursP.ItemsSource = hours;
            minsP.ItemsSource = mins;
            settingsPicker.ItemsSource = settings;
            settingsPicker.SelectedIndex = Preferences.Get("Sort", 0);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).Text.Equals("Add"))
            {
                Job tempJob = new Job()
                {
                    jobName = JobName.Text,
                    oddHrs = this.oddHours.IsChecked,
                    date = this.date.Date,
                    hours = hoursP.SelectedIndex,
                    mins = minsP.SelectedIndex
                };
                
                conn.Insert(tempJob);
                activitiesList.ItemsSource = conn.Table<Job>().ToList();
            } else if (((Button)sender).Text.Equals("Update"))
            {
                Job tempJob = (Job)activitiesList.SelectedItem;

                tempJob.jobName = JobName.Text;
                tempJob.oddHrs = this.oddHours.IsChecked;
                tempJob.date = this.date.Date;
                tempJob.hours = hoursP.SelectedIndex;
                tempJob.mins = minsP.SelectedIndex;
                conn.Update(tempJob);
                activitiesList.ItemsSource = conn.Table<Job>().ToList();

            } else if (((Button)sender).Text.Equals("Delete"))
            {
                conn.Delete((Job)activitiesList.SelectedItem);
                activitiesList.ItemsSource = conn.Table<Job>().ToList();
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                Browser.OpenAsync(new Uri("https://www.miamioh.edu"), BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        private void activitiesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Job tempJob = activitiesList.SelectedItem as Job;
            JobName.Text = tempJob.jobName;
            this.oddHours.IsChecked = tempJob.oddHrs;
            this.date.Date = tempJob.date;
            hoursP.SelectedIndex = tempJob.hours;
            minsP.SelectedIndex = tempJob.mins;
        }

        private void settingsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Preferences.Set("Sort", settingsPicker.SelectedIndex);
        }
    }
}
