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
    public partial class BasicLayoutPage : ContentPage
    {
        public BasicLayoutPage()
        {
            InitializeComponent();
            SizeChanged += HandleSizeChanged;
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            sizeAllocated.Text = String.Format("OnSizeAllocated = {0}x{1}", width, height);
        }
        private void HandleSizeChanged(object sender, EventArgs e)
        {
            sizeChanged.Text = String.Format("SizeChanged = {0}x{1}", Width, Height);
        }
    }
}