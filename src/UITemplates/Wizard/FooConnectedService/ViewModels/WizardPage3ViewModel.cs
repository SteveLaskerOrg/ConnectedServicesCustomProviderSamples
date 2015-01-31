﻿using Microsoft.VisualStudio.ConnectedServices;

namespace Microsoft.ConnectedServices.Samples.ViewModels {
    internal class WizardPage3ViewModel : ConnectedServiceWizardPage {
        public WizardPage3ViewModel() : base() {
            this.Title = "Page 3";
            this.Description = "Page 3 Description";
            this.Legend = "Page 3 Legend";
            this.View = new Views.WizardPage3View();
            this.View.DataContext = this;
            // Disable the page, possibly as a result of Page 2 having errors
            this.IsEnabled = false;
        }
    }
}
