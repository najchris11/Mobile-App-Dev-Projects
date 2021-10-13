using SkiaSharp;
using SkiaSharp.Views.Forms;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthPage : ContentPage
    {

        SQLiteConnection conn;
        string year;
        Dictionary<Int32, Hours> totals = new Dictionary<int, Hours>();
        int totalHrs = 0;
        public MonthPage(string year)
        {
            InitializeComponent();
            this.year = year;
            NavigationPage.SetBackButtonTitle(this, "Month");
            string libFolder = FileSystem.AppDataDirectory;
            string fname = System.IO.Path.Combine(libFolder, "WorkLog.db");
            conn = new SQLiteConnection(fname);
            conn.CreateTable<Job>();

            List<String> totalList = new List<string>();
            foreach (Job j in conn.Query<Job>("select * from job" +
                //" where date >= '1-1-" + year + " 00:00:01' and date < '12-31-" + year + " 23:59:59'" +
                " order by date"))
                
            {
                if (j.date.Year + "" != year) continue;
                if (totals.ContainsKey(j.date.Month))
                {
                    totals[j.date.Month].hoursWorked += j.hours;
                    if (totals[j.date.Month].minsWorked + j.mins >= 60)
                    {
                        totals[j.date.Month].hoursWorked++;
                        totals[j.date.Month].minsWorked = (totals[j.date.Month].minsWorked + j.mins) % 60;
                    }
                    else
                        totals[j.date.Month].minsWorked += j.mins;

                }
                else
                {
                    totals.Add(j.date.Month, new Hours { hoursWorked = j.hours, minsWorked = j.mins });
                    //totals[j.date.Year].hoursWorked = j.hours;
                    //totals[j.date.Year].minsWorked = j.mins;
                }
            }
            foreach (Int32 month in totals.Keys)
            {
                totalList.Add(month<10?"0"+ month + ": " + totals[month] : month + ": " + totals[month]);
                totalHrs += totals[month].hoursWorked;
            }
            this.totalList.ItemsSource = totalList;
        }
        MonthActiv activ;
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

            SKPaint BluePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = Color.Blue.ToSKColor(),
                StrokeWidth = 10
            };

            //canvas.DrawRect(info.Width / 5, (1 - redRatio) * info.Height, info.Width / 5, info.Height, RedPaint);
            //canvas.DrawRect(info.Width / 1.6f, (1 - blueRatio) * info.Height, info.Width / 5, info.Height, BluePaint);
        }
        private void totalList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            activ = new MonthActiv(year, totalList.SelectedItem.ToString().Substring(0, 2));
            Navigation.PushModalAsync(activ, true);
        }
    }
}