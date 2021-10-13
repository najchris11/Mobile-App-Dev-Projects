using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace Prefs {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
			if (!Preferences.ContainsKey("MetricMode"))
				Preferences.Set("MetricMode", false);
			metricSwitch.IsToggled = Preferences.Get("MetricMode", false);
		}
		private void MetricSwitchToggled(object sender, ToggledEventArgs e) {
			Preferences.Set("MetricMode", metricSwitch.IsToggled);
		}
	}
}
