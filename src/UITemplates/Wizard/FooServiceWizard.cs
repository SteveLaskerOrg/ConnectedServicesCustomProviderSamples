using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.ViewModels {
    internal class FooServiceWizard : ConnectedServiceWizard {
        public FooServiceWizard() {
            // Add the ViewModels that represent the pages of the wizard
            this.Pages.Add(new ViewModels.WizardPage1ViewModel(this));
            this.Pages.Add(new ViewModels.WizardPage2ViewModel(this));
            this.Pages.Add(new ViewModels.WizardPage3ViewModel(this));
        }
        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync() {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = "SpecificFooInstance";
            return Task.FromResult(instance);
        }
    }
}