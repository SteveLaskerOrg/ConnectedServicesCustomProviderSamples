using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.ConnectedServiceDemo.ViewModels {
    internal class Page4ViewModel : WizardPage {
        public Page4ViewModel(WizardDemoProvider provider)
            : base(provider) {
            this.Title = "Page 4";
            this.Description = "Page 4 Description";
            this.Legend = "Page 4 Legend";
            this.IsEnabled = true;
            this.View = new Views.Page4();
        }
    }
}