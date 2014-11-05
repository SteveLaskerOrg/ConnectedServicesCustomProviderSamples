using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class WizardPage : NotifyPropertyChangeBase, IConnectedServiceWizardPage
    {
        private WizardDemoProvider provider;

        public WizardPage(WizardDemoProvider provider)
        {
            this.provider = provider;
        }

        public string Title { get; set; }
        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.Set(ref this.description, value); }
        }

        public string Legend { get; set; }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.Set(ref this.isEnabled, value); }
        }

        public bool IsSelected { get; set; }

        public bool HasErrors { get; set; }

        internal bool CanFinish { get; set; }

        public FrameworkElement View { get; set; }

        public Task<NavigationEnabledState> OnPageEntering()
        {
            return Task.FromResult<NavigationEnabledState>(new NavigationEnabledState(null, null, this.CanFinish));
        }

        public Task<WizardNavigationResult> OnPageLeaving()
        {
            this.PageLeavingCount++;

            return Task.FromResult<WizardNavigationResult>(null);
        }

        // this property is used for unit test verification
        public int PageLeavingCount { get; set; }
    }
}
