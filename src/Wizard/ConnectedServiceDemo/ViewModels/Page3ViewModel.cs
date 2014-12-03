using Company.ConnectedServiceDemo.Views;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class Page3ViewModel : WizardPage
    {
        private string text;

        public Page3ViewModel()
            : base()
        {
            this.Title = "Page 3";
            this.Description = "Page 3 Description";
            this.Legend = "Page 3 Legend";
            this.CanFinish = true;
            this.View = new Page3();
            this.View.DataContext = this;
        }

        public string Text
        {
            get { return this.text; }
            set { this.Set(ref this.text, value); }
        }
    }
}