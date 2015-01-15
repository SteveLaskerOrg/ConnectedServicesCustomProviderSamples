using Company.ConnectedServiceDemo.Views;
using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class Page2ViewModel : ConnectedServiceWizardPage
    {
        private string text;

        public Page2ViewModel()
        {
            this.Title = "Page 2";
            this.Description = "Page 2 Description";
            this.Legend = "Page 2 Legend";
            this.View = new Page2();
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