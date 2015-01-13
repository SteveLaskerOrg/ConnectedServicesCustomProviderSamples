using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Threading.Tasks;

namespace ConnectedServiceSinglePageSample
{
    internal class SinglePage : ConnectedServiceSinglePage
    {
        private string name;
        private bool isValid;
        private string extraInformation;
        private string authenticateMessage;
        private Authenticator authenticator;

        public SinglePage()
        {
            this.Title = "Single Page";
            this.Description = "A sample single page Connected Service";
            this.View = new SinglePageView();
            this.View.DataContext = this;

            this.Name = "Default value";
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnNotifyPropertyChanged();
            }
        }

        public bool IsValid
        {
            get { return this.isValid; }
            set
            {
                this.isValid = value;
                this.OnNotifyPropertyChanged();
                this.CalculateIsFinishEnabled();
            }
        }

        public string ExtraInformation
        {
            get { return this.extraInformation; }
            set
            {
                this.extraInformation = value;
                this.OnNotifyPropertyChanged();
            }
        }

        public string AuthenticateMessage
        {
            get { return this.authenticateMessage; }
            set
            {
                this.authenticateMessage = value;
                this.OnNotifyPropertyChanged();
            }
        }

        public Authenticator Authenticator
        {
            get
            {
                if (this.authenticator == null)
                {
                    this.authenticator = new Authenticator();
                    this.authenticator.AuthenticationChanged += Authenticator_AuthenticationChanged;
                    this.CalculateProperties();
                }
                return this.authenticator;
            }
        }

        private void Authenticator_AuthenticationChanged(object sender, AuthenticationChangedEventArgs e)
        {
            this.CalculateProperties();
        }

        private void CalculateProperties()
        { 
            if (this.Authenticator.IsAuthenticated)
            {
                this.AuthenticateMessage = null;
            }
            else
            {
                this.AuthenticateMessage = "Please click 'Sign In'";
            }

            this.CalculateIsFinishEnabled();
        }

        public override Task<ConnectedServiceAuthenticator> CreateAuthenticatorAsync()
        {
            return Task.FromResult<ConnectedServiceAuthenticator>(this.Authenticator);
        }

        private void CalculateIsFinishEnabled()
        {
            this.IsFinishEnabled = this.Authenticator.IsAuthenticated && this.IsValid;
        }

        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = this.Name;
            instance.Metadata.Add("ExtraInfo", this.ExtraInformation);
            return Task.FromResult(instance);
        }
    }
}
