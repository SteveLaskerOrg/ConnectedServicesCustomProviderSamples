using Company.ConnectedServiceDemo.Views;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class Page2ViewModel : WizardPage
    {
        private string text;

        public Page2ViewModel()
            : base()
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
            set { this.Set(ref this.text, value); }
        }
    }
}