using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bindings {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MyData : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private int data;
        public int Value {
            get {
                return data;
            }
            set {
                if (data != value) {
                    data = value;
                    RaisePropertyChanged();
                }
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class VariableToControlBinding : ContentPage {
        MyData myData = new MyData { Value = 0 };
        public VariableToControlBinding() {
            InitializeComponent();
            varValue.SetBinding(Label.TextProperty, new Binding { Source = myData, Path = "Value" });
            BindingContext = this;
            Device.StartTimer(new TimeSpan(0, 0, 1), () => { myData.Value += 1; return true; });
        }
    }
}
