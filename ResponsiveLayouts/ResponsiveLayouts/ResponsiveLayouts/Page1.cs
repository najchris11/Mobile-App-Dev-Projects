using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResponsiveLayouts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private View portraitLayout;
        private View landscapeLayout;
        public Page1()
        {

            StackLayout stackVert = new StackLayout { Orientation = StackOrientation.Vertical };
            stackVert.Children.Add(new Button() { Text = "1" });
            stackVert.Children.Add(new Button() { Text = "2" });
            stackVert.Children.Add(new Button() { Text = "3" });
            stackVert.Children.Add(new Button() { Text = "4" });
            stackVert.Children.Add(new Button() { Text = "5" });
            stackVert.Children.Add(new Button() { Text = "6" });
            portraitLayout = stackVert;

            Grid gridHorz = new Grid();
            gridHorz.Children.Add(new Button() { Text = "1" }, 0, 0);
            gridHorz.Children.Add(new Button() { Text = "2" }, 0, 1);
            gridHorz.Children.Add(new Button() { Text = "3" }, 1, 0);
            gridHorz.Children.Add(new Button() { Text = "4" }, 1, 1);
            gridHorz.Children.Add(new Button() { Text = "5" }, 2, 0);
            gridHorz.Children.Add(new Button() { Text = "6" }, 2, 1);
            landscapeLayout = gridHorz;

        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            bool isNowLandscape = width > height;
            if (isNowLandscape)
            {
                Content = landscapeLayout;
            }
            else
            {
                Content = portraitLayout;
            }
        }
    }
}