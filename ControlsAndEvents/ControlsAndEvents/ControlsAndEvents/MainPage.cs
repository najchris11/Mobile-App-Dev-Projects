using System;
using Xamarin.Forms;

namespace ControlsAndEvents
{
    class MainPage : ContentPage
    {
        Entry word;
        Label wordLengthLabel;

        Button button;
        Label buttonClicks;
        int clickCount = 0;

        Label switchLabel;
        Switch onOff;

        Label stepperLabel;
        Stepper stepper;
        double stepperVal = 0.5;

        Label checkboxLabel;
        CheckBox checkBox;

        Label sliderLabel;
        Slider slider;
        double sliderVal = 0.5;

        public MainPage()
        {
            word = new Entry() { Text = "" };
            wordLengthLabel = new Label() { Text = "" + word.Text.Length };

            button = new Button() { Text = "CLICK ME" };
            buttonClicks = new Label() { Text = "" + clickCount };

            checkBox = new CheckBox() { IsChecked = true };
            checkboxLabel = new Label() { Text = "checked" };

            onOff = new Switch() { IsToggled = true };
            switchLabel = new Label() { Text = "on" };

            stepper = new Stepper() { Maximum = 1, Minimum = 0, Value = 0.5, WidthRequest = 100, Increment = .1 };
            stepperLabel = new Label() { Text = "" + stepperVal };

            slider = new Slider() { BackgroundColor = Color.Gray, Value = 0.5, Minimum = 0, Maximum = 1, WidthRequest = 100 };
            sliderLabel = new Label() { Text = "" + sliderVal };

            StackLayout panel = new StackLayout();
            StackLayout elayer = new StackLayout() { Orientation = StackOrientation.Horizontal };
            StackLayout blayer = new StackLayout() { Orientation = StackOrientation.Horizontal };
            StackLayout clayer = new StackLayout() { Orientation = StackOrientation.Horizontal };
            StackLayout slayer = new StackLayout() { Orientation = StackOrientation.Horizontal };
            StackLayout stlayer = new StackLayout() { Orientation = StackOrientation.Horizontal };
            StackLayout sllayer = new StackLayout() { Orientation = StackOrientation.Horizontal }; 
            elayer.Children.Add(word);
            elayer.Children.Add(wordLengthLabel);
            blayer.Children.Add(button);
            blayer.Children.Add(buttonClicks);
            clayer.Children.Add(checkBox);
            clayer.Children.Add(checkboxLabel);
            slayer.Children.Add(onOff);
            slayer.Children.Add(switchLabel);
            stlayer.Children.Add(stepper);
            stlayer.Children.Add(stepperLabel);
            sllayer.Children.Add(slider);
            sllayer.Children.Add(sliderLabel);
            panel.Padding = new Thickness(20, 30, 20, 0);
            panel.Children.Add(elayer);
            panel.Children.Add(blayer);
            panel.Children.Add(clayer);
            panel.Children.Add(slayer);
            panel.Children.Add(stlayer);
            panel.Children.Add(sllayer);

            this.Content = panel;

            word.TextChanged += entryChanged;
            button.Clicked += OnClicked;
            checkBox.CheckedChanged += OnChecked;
            onOff.Toggled += onToggled;
            stepper.ValueChanged += OnValueChanged;
            slider.ValueChanged += OnValueChanged;

        }

        private void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender == stepper)
            {
                stepperVal = stepper.Value;
                stepperLabel.Text = "" + stepperVal;
            } else if (sender == slider)
            {
                sliderVal = slider.Value;
                sliderLabel.Text = "" + sliderVal;
            }
        }
        private void onToggled(object sender, ToggledEventArgs e)
        {
            if (onOff.IsToggled == true)
            {
                switchLabel.Text = "on";
            }
            else
            {
                switchLabel.Text = "off";
            }
        }
        private void OnChecked(object sender, CheckedChangedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                checkboxLabel.Text = "checked";
            } else
            {
                checkboxLabel.Text = "unchecked";
            }
        }
        private void entryChanged(object sender, TextChangedEventArgs e)
        {
            wordLengthLabel.Text = "" + word.Text.Length;
        }
        private void OnClicked(object sender, EventArgs e)
        {
            clickCount++;
            buttonClicks.Text = "" + clickCount;
        }

    }
}