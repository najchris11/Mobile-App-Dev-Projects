using System;
using Xamarin.Forms;

namespace SecondApp
{
    class MainPage : ContentPage
    {
        Entry num1;
        Entry num2;
        Label num1Label;
        Label num2Label;
        Label totalLabel;
        Button submit;
        
        Label dateLabel;
        DatePicker datePicker;

        Label switchLabel;
        Switch onOff;

        Label stepperLabel;
        Stepper stepper;

        Label progressLabel;
        ProgressBar progressBar;

        Label sliderLabel;
        Slider slider;
        public MainPage()
        {
            num1 = new Entry { Text = "please enter 1 number." };
            num2 = new Entry { Text = "please enter another number." };

            num1Label = new Label() { Text = "First Number" };
            num2Label = new Label() { Text = "Second Number" };
            totalLabel = new Label() { Text = "Total value of First Number and Second Number" };
            dateLabel = new Label() { Text = "Date Picker" };
            switchLabel = new Label() { Text = "Switch" };
            stepperLabel = new Label() { Text = "Stepper" };
            progressLabel = new Label() { Text = "Progress bar" };
            sliderLabel = new Label() { Text = "Slider" };

            submit = new Button() { Text = "Add the two values." };

            datePicker = new DatePicker() { Date = new DateTime(1999, 12, 31) };

            onOff = new Switch() { IsToggled = true };

            stepper = new Stepper();

            progressBar = new ProgressBar();

            slider = new Slider() {BackgroundColor = Color.Gray };

            StackLayout panel = new StackLayout();
            panel.Children.Add(totalLabel);
            panel.Children.Add(num1Label);
            panel.Children.Add(num1);
            panel.Children.Add(num2Label);
            panel.Children.Add(num2);
            panel.Children.Add(submit);
            panel.Children.Add(dateLabel);
            panel.Children.Add(datePicker);
            panel.Children.Add(switchLabel);
            panel.Children.Add(onOff);
            panel.Children.Add(stepperLabel);
            panel.Children.Add(stepper);
            panel.Children.Add(progressLabel);
            panel.Children.Add(progressBar);
            panel.Children.Add(sliderLabel);
            panel.Children.Add(slider);
            panel.Padding = new Thickness(20, 30, 20, 0);

            this.Content = panel;

            submit.Clicked += OnClick;
            

        }
        private void OnClick(object sender, EventArgs e)
        {
            int num1;
            int num2;
            try
            {
                num1 = Int32.Parse(this.num1.Text);
                num2 = Int32.Parse(this.num2.Text);
                totalLabel.Text = "The total of the two numbers is: " + (num1 + num2);
            }
            catch (FormatException)
            {
                totalLabel.Text="Unable to parse numbers. Please ensure that you've provided only numbers";
            }
        }
    }
}
