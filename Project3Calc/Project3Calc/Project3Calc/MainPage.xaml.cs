using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project3Calc
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text.Equals("Clear"))
            {
                Resources["TopNumList"] = "" + 0;
            }
            else if (((Button)sender).Text.Equals("0") && Resources["TopNumList"].Equals("0"))
            {

            }
            else if (Resources["TopNumList"].Equals("0"))
            {
                Resources["TopNumList"] = ((Button)sender).Text;
            }
            else
            {
                Resources["TopNumList"] += ((Button)sender).Text;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

        }
    }
}
