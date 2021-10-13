using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaxFilter
{
    public class ZipData
    {
        public string zip,
            numTaxes,
            pop,
            state,
            city;
        public int total;
        public ZipData(string zip, string city, string state, string pop, string total, string ntax)
        {
            this.zip = zip;
            this.state = state;
            this.pop = pop;
            Int32.TryParse(total, out this.total);
            this.numTaxes = ntax;
            this.city = city;
        }

        public override string ToString()
        {
            return city + ", " + state + " (" + zip + ") $" + total;
        }
    }
    public partial class MainPage : ContentPage
    {
        List<ZipData> db = new List<ZipData>();
        List<String> states = new List<string>();
        Task loadingTask;
        ListView lv = new ListView();
        Entry zipE;
        Label zipL, stateL, salL, salL2, rangL, rangL2, res;
        Button filter;
        Slider salS, rangS;
        Picker stateP;
        public MainPage()
        {
            loadingTask = GetFileContents("zipcodes.tsv");
            ParseData("", "", "", "");
            StackLayout layout = new StackLayout();


            StackLayout horz1 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            zipL = new Label() { Text = "Zip code" };
            zipE = new Entry();
            horz1.Children.Add(zipL);
            horz1.Children.Add(zipE);

            StackLayout horz2 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            stateL = new Label() { Text = "State" };
            states.Add("");
            if (!loadingTask.IsCompleted)
            {
                loadingTask.Wait();
            }
            stateP = new Picker() { ItemsSource = states };
            horz2.Children.Add(stateL);
            horz2.Children.Add(stateP);

            StackLayout horz3 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            salL = new Label() { Text = "Minimum Salary" };
            salS = new Slider(0, 1000000, 0);
            salL2 = new Label() { Text = "$" + salS.Value };
            horz3.Children.Add(salL);
            horz3.Children.Add(salS);
            horz3.Children.Add(salL2);

            StackLayout horz4 = new StackLayout() { Orientation = StackOrientation.Horizontal };
            rangL = new Label() { Text = "Salary range" };
            rangS = new Slider() { Minimum = 0, Maximum = 5000, Value = 50, WidthRequest = 150 };
            rangL2 = new Label() { Text = "$" + rangS.Value };
            horz4.Children.Add(rangL);
            horz4.Children.Add(rangS);
            horz4.Children.Add(rangL2);

            filter = new Button() { Text = "FILTER" };
            res = new Label() { Text = salS.Value + " <= income < " + ((Int32)(salS.Value + rangS.Value)) };

            layout.Children.Add(horz1);
            layout.Children.Add(horz2);
            layout.Children.Add(horz3);
            layout.Children.Add(horz4);
            layout.Children.Add(filter);
            layout.Children.Add(res);
            layout.Children.Add(lv);
            layout.Padding = new Thickness(0, 40, 0, 0);
            Content = layout;

            zipE.TextChanged += entryChanged;
            stateP.SelectedIndexChanged += picker_selectedIndexChanged;
            salS.ValueChanged += OnValueChanged;
            rangS.ValueChanged += OnValueChanged;
        }

        private async Task GetFileContents (string fname)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            string namespaceName = "TaxFilter";
            Stream stream = assembly.GetManifestResourceStream(namespaceName + "." + fname);
            using (var dictReader = new System.IO.StreamReader(stream))
            {
                dictReader.ReadLine();
                while (!(dictReader.EndOfStream))
                {
                    string line = await dictReader.ReadLineAsync().ConfigureAwait(false);
                    string[] tabLine = line.Split('\t');
                    db.Add(new ZipData(tabLine[1], tabLine[3], tabLine[4], tabLine[17], tabLine[18], tabLine[16]));
                    if (!(states.Contains(tabLine[4]))) states.Add(tabLine[4]);
                }
            }
        }

        private void ParseData(string zip, string state, string low, string high)
        {
            int highI, lowI;
            Int32.TryParse(low, out lowI);
            Int32.TryParse(high, out highI);
            List<ZipData> ret = new List<ZipData>();
            if (!(loadingTask.IsCompleted))
            {
                loadingTask.Wait();
            }
  
                foreach (ZipData z in db)
                {
                    if (z.zip.StartsWith(zip) && (z.state.Equals(state) || z.state.Equals("")) && (lowI <= z.total && z.total < highI))
                    {
                        ret.Add(z);
                    }
                }
                
                lv.ItemsSource = ret;
            
        }
        private void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender == salS)
            {
                if (!(loadingTask.IsCompleted))
                {
                    loadingTask.Wait();
                }
                ParseData(zipE.Text, states[stateP.SelectedIndex + 1], "" + salS.Value, "" + (salS.Value + rangS.Value));
                res = new Label() { Text = salS.Value + " <= income < " + ((Int32)(salS.Value + rangS.Value)) };
            }
            else if (sender == rangS)
            {
                if (!(loadingTask.IsCompleted))
                {
                    loadingTask.Wait();
                }
                ParseData(zipE.Text, states[stateP.SelectedIndex + 1], "" + salS.Value, "" + (salS.Value + rangS.Value));
                rangL2 = new Label() { Text = "$" + rangS.Value };
                res = new Label() { Text = salS.Value + " <= income < " + ((Int32)(salS.Value + rangS.Value)) };
            }
        }
        private void entryChanged(object sender, TextChangedEventArgs e)
        {
            if (!(loadingTask.IsCompleted))
            {
                loadingTask.Wait();
            }
            ParseData(zipE.Text, states[stateP.SelectedIndex + 1], "" + salS.Value, "" + (salS.Value + rangS.Value));
        }
        private void OnClicked(object sender, EventArgs e)
        {
            if (!(loadingTask.IsCompleted))
            {
                loadingTask.Wait();
            }
            ParseData(zipE.Text, states[stateP.SelectedIndex + 1], "" + salS.Value, "" + (salS.Value + rangS.Value));
        }
        private void picker_selectedIndexChanged(object sender, EventArgs e)
        {
            if (!(loadingTask.IsCompleted))
            {
                loadingTask.Wait();
            }
            ParseData(zipE.Text, states[stateP.SelectedIndex + 1], "" + salS.Value, "" + (salS.Value + rangS.Value));
        }
    }
}
