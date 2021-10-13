using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAML
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Example3 : ContentPage
    {
        public Example3()
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
    }
}