using Company.ConnectedServiceDemo.ViewModels;
using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo
{
    internal class WizardDemoProviderWizard : ConnectedServiceWizard
    {
        public WizardDemoProviderWizard()
        {
            this.Pages.Add(new Page1ViewModel());
            this.Pages.Add(new Page2ViewModel());
            this.Pages.Add(new Page3ViewModel());
        }

        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            Page1ViewModel page1 = (Page1ViewModel)this.Pages[0];
            Page2ViewModel page2 = (Page2ViewModel)this.Pages[1];
            Page3ViewModel page3 = (Page3ViewModel)this.Pages[2];

            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = "wizard";
            instance.Metadata.Add("Page1.Text", page1.Text);
            instance.Metadata.Add("Page2.Text", page2.Text);
            instance.Metadata.Add("Page3.Text", page3.Text);
            return Task.FromResult(instance);
        }
    }
}
