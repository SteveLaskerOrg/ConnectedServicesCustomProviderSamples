﻿using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.UITemplates.Wizard.ViewModels
{
    internal class ServiceWizard : ConnectedServiceWizard
    {
        public ServiceWizard()
        {
            // Add the ViewModels that represent the pages of the wizard
            this.Pages.Add(new ViewModels.WizardPage1ViewModel(this));
            this.Pages.Add(new ViewModels.WizardPage2ViewModel(this));
            this.Pages.Add(new ViewModels.WizardPage3ViewModel(this));
        }
        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = "SpecificFooInstance";
            return Task.FromResult(instance);
        }
    }
}