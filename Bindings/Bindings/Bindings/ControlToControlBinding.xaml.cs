﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bindings {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ControlToControlBinding : ContentPage {
        public ControlToControlBinding() {
            InitializeComponent();
            Binding picker = new Binding();
            picker.Mode = BindingMode.OneWay;
        }
    }
}