using Company.ConnectedServiceDemo.ViewModels;
using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo
{
    internal class WizardDemoProviderWizard : IConnectedServiceProviderWizardUI
    {
        private ObservableCollection<IConnectedServiceWizardPage> pages;

        public event EventHandler<EnableNavigationEventArgs> EnableNavigation;

        public WizardDemoProviderWizard()
        {
            this.pages = new ObservableCollection<IConnectedServiceWizardPage>();
            this.pages.Add(new Page1ViewModel());
            this.pages.Add(new Page2ViewModel());
            this.pages.Add(new Page3ViewModel());
        }

        public ObservableCollection<IConnectedServiceWizardPage> Pages
        {
            get { return this.pages; }
        }

        public Task<IConnectedServiceInstance> GetFinishedServiceInstance()
        {
            Page1ViewModel page1 = (Page1ViewModel)this.pages[0];
            Page2ViewModel page2 = (Page2ViewModel)this.pages[1];
            Page3ViewModel page3 = (Page3ViewModel)this.pages[2];

            WizardDemoInstance instance = new WizardDemoInstance("wizard", "wizard",
                       "Microsoft.VisualStudio.ConnectedServices.Sample.WizardProvider",
                       "Page1.Text", page1.Text,
                       "Page2.Text", page2.Text,
                       "Page3.Text", page3.Text);

            return Task.FromResult<IConnectedServiceInstance>(instance);
        }

        internal void OnEnableNavigation(NavigationEnabledState state)
        {
            if (this.EnableNavigation != null)
            {
                this.EnableNavigation(this, new EnableNavigationEventArgs() { State = state });
            }
        }
    }
}
