using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ButtonClicks {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		int cnt = 0;
		public MainPage() {
			InitializeComponent();
		}
		private void OnClicked(object sender, EventArgs e) {
			cnt++;
			label.Text = cnt.ToString();
		}
	}
}
