﻿using Company.ConnectedServiceDemo.Views;
using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class Page3ViewModel : ConnectedServiceWizardPage
    {
        private string text;

        public Page3ViewModel()
        {
            this.Title = "Page 3";
            this.Description = "Page 3 Description";
            this.Legend = "Page 3 Legend";
            this.View = new Page3();
            this.View.DataContext = this;
            this.IsEnabled = false;
        }

        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                this.OnNotifyPropertyChanged();
            }
        }

        public override Task<NavigationEnabledState> OnPageEnteringAsync(WizardEnteringArgs args)
        {
            // The Wizard can be finished from this page, since it is the last page
            return Task.FromResult(new NavigationEnabledState(null, null, true));
        }
    }
}