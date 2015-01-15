using Company.ConnectedServiceDemo.Views;
using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class Page1ViewModel : ConnectedServiceWizardPage
    {
        private string text;

        public Page1ViewModel()
        {
            this.Title = "Page 1";
            this.Description = "Page 1 Description";
            this.Legend = "Page 1 Legend";
            this.View = new Page1();
            this.View.DataContext = this;
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
            return Task.FromResult(new NavigationEnabledState(null, null, false));
        }
    }
}
