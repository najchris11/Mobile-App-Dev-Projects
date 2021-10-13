using SQLite;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MyData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Stopwatch sw = new Stopwatch();
        public Stopwatch Value
        {
            get
            {
                return sw;
            }
            set { if (sw != value)
                {
                    sw = value;
                    RaisePropertyChanged();
                } }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class MainPage : TabbedPage
    {
        SQLiteConnection conn;
        ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        MyData mySW = new MyData();
        DateTime rateDate = DateTime.Today;
        
        
        int score = 0;


        public MainPage()
        {
            InitializeComponent();
            CurrentPage = Children[1];

            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "TaskLog.db");
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Task>();

            swLabel.SetBinding(Label.TextProperty, new Binding { Source = mySW, Path = "Value.Elapsed" });
            BindingContext = this;

            Date.SetBinding(Label.TextProperty, new Binding { Source = rateDate, Path = "Date" });
            Date.BindingContext = this;

            Rating.SetBinding(Label.TextProperty, new Binding { Source = score, Mode = BindingMode.TwoWay });
            Rating.BindingContext = this;
            
            Load("payment_success.m4a");
            popLists();
        }
        private void popLists()
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

            List<Task> taskList = new List<Task>();
            foreach (Task t in conn.Table<Task>().ToList())
            {
                if (t.isComplete) continue;
                taskList.Add(t);
            }
            List.ItemsSource = taskList;
            taskList.Clear();
            foreach (Task task in conn.Table<Task>().ToList())
            {
                if (task.isComplete) continue;
                taskList.Add(task);
            }
            taskSelect.ItemsSource = taskList;
            if (!Preferences.ContainsKey("Hour")) Preferences.Set("Hour", false);
            if (!Preferences.ContainsKey("Sound")) Preferences.Set("Sound", true);
            hourS.IsToggled = Preferences.Get("Hour", false);
            soundS.IsToggled = Preferences.Get("Sound", true);

            leftButton.Source = ImageSource.FromUri(new Uri("https://www.nicepng.com/png/detail/405-4058644_long-arrow-left-comments-long-left-arrow-icon.png"));
            rightButton.Source = ImageSource.FromUri(new Uri("https://simg.nicepng.com/png/small/40-405352_long-arrow-pointing-to-the-right-long-arrow.png"));

            List<string> rList = new List<string>();
            int j = 0;
            score = 0;
            foreach (Task t in conn.Table<Task>().ToList())
            {
                if (t.mins == 0 && t.hours == 0)
                {
                    rList.Add(t.taskName + " - " + 0);
                    continue;
                }
                int tScore = ((int)t.timeElapsed.Minutes < (t.hours * 60 + t.mins)) ? 100 : (int)t.timeElapsed.Minutes / (t.hours * 60 + t.mins);
                if (t.date == rateDate && t.isComplete && t.taskCategory == null)
                {
                    rList.Add(t.taskName + " - " + tScore);
                    score += tScore;
                    j++;
                }
                else if (t.date == rateDate && t.isComplete)
                {
                    rList.Add(t.taskName + " in " + t.taskCategory + " - " + tScore);
                    j++;
                    score += tScore;
                }
            }
            ratingList.ItemsSource = rList;
            if (score != 0) score /= j;
            else score = 100;
        }

        private void Load(string file)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            String ns = "FinalProject";
            Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
            player.Load(audioStream);
        }

        
        private void SubmitTasks(object sender, EventArgs e)
        {
            Task t = new Task
            {
                taskName = taskName.Text,
                taskCategory = catName.Text,
                isActivity = isActivity.IsChecked,
                hours = hoursP.SelectedIndex,
                mins = minsP.SelectedIndex,
                priority = (int)Priority.Value,
                date = date.Date
            };
            conn.Insert(t);
            List<Task> taskList = new List<Task>();
            foreach (Task task in conn.Table<Task>().ToList())
            {
                if (task.isComplete) continue;
                taskList.Add(task);
            }
            List.ItemsSource = taskList;
            taskList.Clear();
            foreach (Task task in conn.Table<Task>().ToList())
            {
                if (task.isComplete) continue;
                taskList.Add(task);
            }
            taskSelect.ItemsSource = taskList;

        }

        private void soundS_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("Sound", soundS.IsToggled);
        }

        private void hourS_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("Hour", hourS.IsToggled);
        }

        private void SWStart(object sender, EventArgs e)
        {
            mySW.Value.Start();           
        }

        private void SWStop(object sender, EventArgs e)
        {
            Task t = (Task)taskSelect.SelectedItem;
            mySW.Value.Stop();
            t.timeElapsed = mySW.Value.Elapsed;
        }

        private void SWEnd(object sender, EventArgs e)
        {
            Task t = (Task)taskSelect.SelectedItem;
            mySW.Value.Stop();
            t.timeElapsed = mySW.Value.Elapsed;
            mySW.Value.Reset();
            t.isComplete = true;
            conn.Execute("UPDATE tasks set isComplete = true where _id = ?", t.Id);
            List<Task> taskList = new List<Task>();
            foreach (Task task in conn.Table<Task>().ToList())
            {
                if (task.isComplete) continue;
                taskList.Add(task);
            }
            taskSelect.ItemsSource = taskList;
            makeSound(t);
        }

        private void makeSound(Task t)
        {
            if (!soundS.IsToggled) return;
            if (t.timeElapsed.TotalMinutes < (t.hours*60 + t.mins))
            {
                player.Play();
            }
        }

        private void leftButton_Clicked(object sender, EventArgs e)
        {
            DateTime newDate = rateDate.AddDays(-1);
            //rateDate = rateDate.AddDays(1);
            List<string> rList = new List<string>();
            int i = 0;
            score = 0;
            foreach (Task t in conn.Table<Task>().ToList())
            {
                if (t.mins == 0 && t.hours == 0)
                {
                    rList.Add(t.taskName + " - " + 0);
                    continue;
                }
                int tScore = ((int)t.timeElapsed.Minutes < (t.hours * 60 + t.mins)) ? 100 : (int)t.timeElapsed.Minutes / (t.hours * 60 + t.mins);
                if (t.date == newDate && t.isComplete && t.taskCategory == null)
                {
                    rList.Add(t.taskName + " - " + tScore);
                    score += tScore;
                    i++;
                }
                else if (t.date == newDate && t.isComplete)
                {
                    rList.Add(t.taskName + " in " + t.taskCategory + " - " + tScore);
                    i++;
                    score += tScore;
                }
            }
            rateDate = newDate;
            ratingList.ItemsSource = rList;
            if (score != 0) score /= i;
            else score = 100;
        }

        private void rightButton_Clicked(object sender, EventArgs e)
        {
            DateTime newDate = rateDate.AddDays(1);
            //rateDate = rateDate.AddDays(1);
            List<string> rList = new List<string>();
            int i = 0;
            score = 0;
            foreach (Task t in conn.Table<Task>().ToList())
            {
                if (t.mins == 0 && t.hours == 0)
                {
                    rList.Add(t.taskName + " - " + 0);
                    continue;
                }
                int tScore = ((int)t.timeElapsed.Minutes < (t.hours * 60 + t.mins)) ? 100 : (int)t.timeElapsed.Minutes / (t.hours * 60 + t.mins);
                if (t.date == newDate && t.isComplete && t.taskCategory == null)
                {
                    rList.Add(t.taskName + " - " + tScore);
                    score += tScore;
                    i++;
                }
                else if (t.date == newDate && t.isComplete)
                {
                    rList.Add(t.taskName + " in " + t.taskCategory + " - " + tScore);
                    i++;
                    score += tScore;
                }
            }
            rateDate = newDate;
            ratingList.ItemsSource = rList;
            if (score != 0) score /= i;
            else score = 100;
        }
    }
}
