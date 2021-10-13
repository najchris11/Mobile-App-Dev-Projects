using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using SQLite;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace Project5WorkLog
{
    public partial class MainPage : TabbedPage
    {
        SQLiteConnection conn;
        int totalHrs = 0;
        Dictionary<Int32, Hours> totals = new Dictionary<int, Hours>();
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Home");
            if (!Preferences.ContainsKey("Sort")) Preferences.Set("Sort", 0);
            addLists();

            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "WorkLog.db");
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Job>();
            activitiesList.ItemsSource = conn.Table<Job>().ToList();
            
            List<String> totalList = new List<string>();
            foreach (Job j in conn.Query<Job>("select * from job order by date"))
            {
                if (totals.ContainsKey(j.date.Year))
                {
                    totals[j.date.Year].hoursWorked += j.hours;
                    if (totals[j.date.Year].minsWorked + j.mins >= 60)
                    {
                        totals[j.date.Year].hoursWorked++;
                        totals[j.date.Year].minsWorked = (totals[j.date.Year].minsWorked + j.mins)%60;
                    } else
                        totals[j.date.Year].minsWorked += j.mins;

                }
                else
                {
                    totals.Add(j.date.Year, new Hours { hoursWorked = j.hours, minsWorked = j.mins });
                    //totals[j.date.Year].hoursWorked = j.hours;
                    //totals[j.date.Year].minsWorked = j.mins;
                }
            }
            foreach (Int32 year in totals.Keys)
            {
                totalList.Add(year + ": " + totals[year]);
                totalHrs += totals[year].hoursWorked;
            }
            this.totalList.ItemsSource = totalList;
            //GenerateWorkData(2018, 5);
        }
        MonthPage month;
        private void addLists()
        {
            List<int> hours = new List<int>();
            List<int> mins = new List<int>();
            List<string> settings = new List<string>();

            for (int i = 0; i <= 24; i++)
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
            }
            else if (((Button)sender).Text.Equals("Update"))
            {
                Job tempJob = (Job)activitiesList.SelectedItem;

                tempJob.jobName = JobName.Text;
                tempJob.oddHrs = this.oddHours.IsChecked;
                tempJob.date = this.date.Date;
                tempJob.hours = hoursP.SelectedIndex;
                tempJob.mins = minsP.SelectedIndex;
                conn.Update(tempJob);
                activitiesList.ItemsSource = conn.Table<Job>().ToList();

            }
            else if (((Button)sender).Text.Equals("Delete"))
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

        private void totalList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            month = new MonthPage(totalList.SelectedItem.ToString().Substring(0, 4));
            Navigation.PushModalAsync(month, true);
        }
        public void GenerateWorkData(int startYear, int numYears)
        {
            Random rng = new Random(34);
            String[] sites = { "Walmart", "ACE", "Miami", "Dubois", "Chipolte" };
            for (int dy = 0; dy < numYears; dy++)
            {
                TimeSpan ts = new TimeSpan();
                int year = startYear + dy;
                for (int month = 1; month <= 12; month++)
                {
                    int N = 3;
                    if (month == 1 || year == 2021)
                        N = 1;
                    for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
                    {
                        int numJobs = rng.Next(N) + 1;
                        DateTime date = new DateTime(year, month, day);
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                            numJobs = rng.Next(5) == 0 ? 1 : 0;
                        for (int job = 0; job < numJobs; job++)
                        {
                            int hours = rng.Next(8);
                            int minutes = rng.Next(4) * 15;
                            String site = sites[rng.Next(sites.Length)];
                            TimeSpan jobTime = new TimeSpan(hours, minutes, 0);
                            bool isOdd = rng.Next(5) == 0 || (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
                            // Don’t print, but put it into a list (or DB).
                            //Console.WriteLine("Job site: " + site);


                            //Console.WriteLine("Date of job: " + date);


                            //Console.WriteLine("Time worked: " + jobTime );


                            //Console.WriteLine("Odd hours?: " + isOdd);
                            Job j = new Job { date = date, hours = hours, jobName = site, mins = minutes, oddHrs = isOdd };
                            conn.Insert(j);
                        }
                    }
                }
                //Console.WriteLine(ts);
            }

        }
        void OnCanvas1ViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            SKPaint RedPaint = new SKPaint
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = Color.Red.ToSKColor(),
                StrokeWidth = 3
            };
            int i = 1;
            //foreach (Int32 year in totals.Keys)
            //{
            //    canvas.DrawRect((info.Width / totals.Keys.Count * i), (1 - (totals[year].hoursWorked / totalHrs)) * info.Height, info.Width / totals[year].hoursWorked, info.Height, RedPaint);
            //    i++;
            //}
            canvas.DrawRect((info.Width / totals.Keys.Count * i), (1 - (totals[2018].hoursWorked / totalHrs)) * info.Height, info.Width / totals[2018].hoursWorked, info.Height, RedPaint);
            RepaintScreen();

            //SKPaint BluePaint = new SKPaint
            //{
            //    Style = SKPaintStyle.Fill,
            //    Color = Color.Blue.ToSKColor(),
            //    StrokeWidth = 10
            //};
            
            //canvas.DrawRect(info.Width / 5, (1 - redRatio) * info.Height, info.Width / 5, info.Height, RedPaint);
            //canvas.DrawRect(info.Width / 1.6f, (1) * info.Height, info.Width / 5, info.Height, BluePaint);
        }
        void RepaintScreen()
        {
            view1.InvalidateSurface();
        }
    }
}

   

