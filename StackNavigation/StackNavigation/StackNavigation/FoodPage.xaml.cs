using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StackNavigation {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage {
        public bool valid;
        public string whichFood;
        public FoodPage(string defaultFood) {
            InitializeComponent();
            StackLayout stk = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children = {
                    new Label { Text = "Food", FontSize=40, HorizontalOptions=LayoutOptions.StartAndExpand },
                    new Image { Source="food.png", HeightRequest=40, HorizontalOptions=LayoutOptions.EndAndExpand },
                }
            };
            NavigationPage.SetTitleView(this, stk);
            food.SelectedItem = defaultFood;
            if (food.SelectedIndex == -1)
                food.SelectedIndex = 0;
        }
        private void AcceptClicked(object sender, EventArgs e) {
            valid = true;
            if (food.SelectedIndex != -1) {
                whichFood = (string)food.SelectedItem;
            }
            Navigation.PopAsync();
        }
        private void CancelClicked(object sender, EventArgs e) {
            valid = false;
            Navigation.PopAsync();
        }
    }
}