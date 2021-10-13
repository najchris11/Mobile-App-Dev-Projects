using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PickerDemo {
	//public class Student {
	//	public String name;
	//	public int grade;
	//	public Student(String n, int g) {
	//		name = n;
	//		grade = g;
	//	}
	//	public override string ToString() {
	//		return String.Format("{0} ({1})", name, grade);
	//	}
	//}
	public class School
	{
		public string Name { get; set; }
		public string WebsiteURL { get; set; }
		public Color SchoolColor { get; set; }
		public Color School2ndColor { get; set; }
	}
	public class MainPage : ContentPage {
		//Label sizeLabel = new Label { Text = "Medium" };
		//Label colorLabel = new Label { Text = "Blue" };
		//Label studentLabel = new Label { Text = "n/a" };
		//Picker sizePicker;
		//Picker colorPicker;
		//Picker studentPicker;

		public ObservableCollection<School> schoolList = new ObservableCollection<School> {
				new School { Name="Miami", WebsiteURL="https://www.miamioh.edu", SchoolColor=Color.Red, School2ndColor= Color.Black},
				new School { Name="Ohio State", WebsiteURL="https://www.osu.edu", SchoolColor=Color.Red, School2ndColor=Color.Gray},
				new School { Name="U. Cincinnati", WebsiteURL="https://www.uc.edu", SchoolColor=Color.Red, School2ndColor=Color.Black },
				new School { Name="Ohio", WebsiteURL="https://www.ohio.edu", SchoolColor=Color.Green, School2ndColor=Color.Gold },
		};

		Picker picker;
		WebView wv;

		public MainPage() {
			StackLayout panel = new StackLayout();

			picker = new Picker();
			foreach (School s in schoolList)
            {
				picker.Items.Add(s.Name);
            }
			picker.SelectedIndex = 1;
			picker.SelectedIndexChanged += picker_selectedIndexChanged;
			//sizePicker = new Picker();
			//sizePicker.Items.Add("X Small");
			//sizePicker.Items.Add("Small");
			//sizePicker.Items.Add("Medium");
			//sizePicker.Items.Add("Large");
			//sizePicker.Items.Add("X Large");
			//sizePicker.SelectedIndex = 2;
			//sizePicker.SelectedIndexChanged += SizePicker_SelectedIndexChanged;

			//colorPicker = new Picker();
			//colorPicker.Items.Add("Red");
			//colorPicker.Items.Add("Green");
			//colorPicker.Items.Add("Blue");
			//colorPicker.SelectedIndex = 2;
			//colorPicker.SelectedIndexChanged += ColorPicker_SelectedIndexChanged;

			//studentPicker = new Picker();
			//List<Student> students = new List<Student>();
			//students.Add(new Student("Maria", 6));
			//students.Add(new Student("Alberto", 5));
			//students.Add(new Student("Greta", 3));
			//studentPicker.ItemsSource = students;
			//studentPicker.SelectedIndex = 0;
			//studentPicker.SelectedIndexChanged += StudentPicker_SelectedIndexChanged;

			//StackLayout sizePanel = new StackLayout();
			//sizePanel.Children.Add(sizeLabel);
			//sizePanel.Children.Add(sizePicker);

			//StackLayout colorPanel = new StackLayout();
			//colorPanel.Children.Add(colorLabel);
			//colorPanel.Children.Add(colorPicker);

			//StackLayout studentPanel = new StackLayout();
			//studentPanel.Children.Add(studentLabel);
			//studentPanel.Children.Add(studentPicker);

			//Frame sizeFrame = new Frame();
			//sizeFrame.Content = sizePanel;
			//sizeFrame.BorderColor = Color.Black;

			//Frame colorFrame = new Frame();
			//colorFrame.Content = colorPanel;
			//colorFrame.BorderColor = Color.Black;

			//Frame studentFrame = new Frame();
			//studentFrame.Content = studentPanel;
			//studentFrame.BorderColor = Color.Black;

			//panel.Children.Add(sizeFrame);
			//panel.Children.Add(colorFrame);
			//panel.Children.Add(studentFrame);

			CreateWebView(out wv, schoolList[1].WebsiteURL);

			panel.Children.Add(picker);
			panel.Children.Add(wv);
			Content = panel;
		}

		private void picker_selectedIndexChanged(object sender, EventArgs e)
        {
			wv.Source = schoolList[((Picker)sender).SelectedIndex].WebsiteURL;
        }
		//private void StudentPicker_SelectedIndexChanged(object sender, EventArgs e) {
		//	Picker picker = (Picker)sender;
		//	Student whichStudent = (Student)picker.SelectedItem;
		//	studentLabel.Text = whichStudent.ToString();
		//}
		//private void SizePicker_SelectedIndexChanged(object sender, EventArgs e) {
		//	sizeLabel.Text = (String)sizePicker.SelectedItem;
		//}
		//private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e) {
		//	colorLabel.Text = (String)colorPicker.SelectedItem;
		//}

		public void CreateWebView(out WebView wv, string URL)
		{

			wv = new WebView
			{
				Source = new UrlWebViewSource
				{
					Url = URL
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 500
			};


		}
	}
}
