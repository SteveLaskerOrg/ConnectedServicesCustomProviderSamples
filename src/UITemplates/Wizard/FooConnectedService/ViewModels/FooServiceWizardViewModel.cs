using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.ViewModels
{
    internal class FooServiceWizardViewModel : ConnectedServiceWizard
    {
        public FooServiceWizardViewModel()
        {
            // Add the ViewModels that represent the pages of the wizard
            this.Pages.Add(new ViewModels.WizardPage1ViewModel());
            this.Pages.Add(new ViewModels.WizardPage2ViewModel());
            this.Pages.Add(new ViewModels.WizardPage3ViewModel());
        }
        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = "SpecificFooInstance";
            return Task.FromResult(instance);
        }
    }
}
