using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;

namespace AudioPlayer
{
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
		public MainPage()
		{
			InitializeComponent();
			Load("start.mp3");
		}
		private void Load(string file)
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			String ns = "AudioPlayer";
			Stream audioStream = assembly.GetManifestResourceStream(ns + "." + file);
			player.Load(audioStream);
		}
		private void OnPlayClicked(object sender, EventArgs e)
		{
			player.Play();
		}
		private void OnPauseClicked(object sender, EventArgs e)
		{
			player.Pause();
		}
		private void Switch_Toggled(object sender, EventArgs e)
        {
			if (loopswitch.IsEnabled) { player.Loop = true; }
			else { player.Loop = false; }
        }
	}
}
