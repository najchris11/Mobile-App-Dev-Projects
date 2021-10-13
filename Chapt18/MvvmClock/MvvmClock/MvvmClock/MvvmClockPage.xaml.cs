using System;
using Xamarin.Forms;

namespace MvvmClock
{
    public partial class MvvmClockPage : ContentPage
    {
        public MvvmClockPage()
        {
            InitializeComponent();
            DateTime start = DateTime.Now;
            TimeSpan diff = start - DateTime.Now;
            FindByName("dateTimeViewModel").
        }
    }
}
