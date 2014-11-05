using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.ConnectedServiceDemo.ViewModels {
    internal class Page2ViewModel : WizardPage {
        public Page2ViewModel(WizardDemoProvider provider)
            : base(provider) {
            this.Title = "Page 2";
            this.Description = "Page 2 Description";
            this.Legend = "Page 2 Legend";
            this.IsEnabled = true;
            this.View = new Views.Page2();
        }
    }
}