using Company.ConnectedServiceDemo.Views;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class Page1ViewModel : WizardPage
    {
        private string text;

        public Page1ViewModel()
            : base()
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
            set { this.Set(ref this.text, value); }
        }
    }
}
