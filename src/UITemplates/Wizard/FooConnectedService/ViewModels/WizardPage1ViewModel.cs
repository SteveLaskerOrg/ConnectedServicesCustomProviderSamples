﻿using Microsoft.VisualStudio.ConnectedServices;

namespace Microsoft.ConnectedServices.Samples.ViewModels {
    internal class WizardPage1ViewModel : ConnectedServiceWizardPage {
        public WizardPage1ViewModel() : base() {
            this.Title = "Page 1";
            this.Description = "Page 1 Description";
            this.Legend = "Page 1 Legend";
            this.View = new Views.WizardPage1Viiew();
            this.View.DataContext = this;
        }
    }
}