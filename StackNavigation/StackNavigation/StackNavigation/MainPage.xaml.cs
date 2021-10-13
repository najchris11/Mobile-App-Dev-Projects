using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StackNavigation {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage(){
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Home");
        }
        RegistrationPage reg;
        FoodPage food;
        AboutPage about;
        protected override void OnAppearing() {
            base.OnAppearing();
            if (reg != null) {
                label.Text = reg.Name;
                reg = null;
            } else if (food != null && food.valid) {
                label.Text = food.whichFood;
                food = null;
            } else if (about != null) {
                label.Text = "About";
                about = null;
            }
        }
        private async void RegistrationClicked(object sender, EventArgs e) {
            reg = new RegistrationPage();
            await Navigation.PushAsync(reg, true);
        }
        private async void FoodClicked(object sender, EventArgs e) {
            food = new FoodPage("Pasta");
            await Navigation.PushAsync(food, true);
        }
        private async void AboutClicked(object sender, EventArgs e) {
            about = new AboutPage();
            await Navigation.PushModalAsync(about, true);
        }
    }
}
