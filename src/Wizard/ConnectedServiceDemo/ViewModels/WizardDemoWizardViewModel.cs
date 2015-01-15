using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class WizardDemoWizardViewModel : ConnectedServiceWizard
    {
        public WizardDemoWizardViewModel()
        {
            this.Pages.Add(new Page1ViewModel());
            this.Pages.Add(new Page2ViewModel());
            this.Pages.Add(new Page3ViewModel());
        }

        /// <summary>
        /// This method is called when the user clicks the Finish button.
        /// It returns the 'finished' ConnectedServiceInstance that will be passed to the Handler.
        /// </summary>
        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            Page1ViewModel page1 = (Page1ViewModel)this.Pages[0];
            Page2ViewModel page2 = (Page2ViewModel)this.Pages[1];
            Page3ViewModel page3 = (Page3ViewModel)this.Pages[2];

            ConnectedServiceInstance instance = new ConnectedServiceInstance();

            // Adding some data that will be available to the ConnectedServiceHandler.
            // The values that the user entered for these fields will be used in the generated
            // code file.
            instance.Metadata.Add("Page1.Text", page1.Text);
            instance.Metadata.Add("Page2.Text", page2.Text);
            instance.Metadata.Add("Page3.Text", page3.Text);

            return Task.FromResult(instance);
        }
    }
}
