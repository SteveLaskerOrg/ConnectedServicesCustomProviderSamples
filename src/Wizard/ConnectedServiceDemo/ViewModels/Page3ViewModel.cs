using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.ConnectedServiceDemo.ViewModels {
    internal class Page3ViewModel : WizardPage {
        public Page3ViewModel(WizardDemoProvider provider)
            : base(provider) {
            this.Title = "Page 3";
            this.Description = "Page 3 Description";
            this.Legend = "Page 3 Legend";
            this.IsEnabled = true;
            this.View = new Views.Page3();
        }
    }
}