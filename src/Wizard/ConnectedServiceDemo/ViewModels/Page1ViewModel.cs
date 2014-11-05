using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.ConnectedServiceDemo.ViewModels {
    internal class Page1ViewModel : WizardPage {
        public Page1ViewModel(WizardDemoProvider provider)
            :base(provider) {
            this.Title = "Page 1";
            this.Description = "Page 1 Description";
            this.Legend = "Page 1 Legend";
            this.IsEnabled = true;
            this.View = new Views.Page1();
        }
    }
}
