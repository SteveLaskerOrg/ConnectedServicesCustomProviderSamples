using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace ConnectedServiceSinglePageSample
{
    internal class SinglePage : ConnectedServiceSinglePage
    {
        private string serviceName;
        private string extraInformation;
        private string authenticateMessage;
        private Authenticator authenticator;

        public SinglePage()
        {
            this.Title = "Single Page";
            this.Description = "A sample single page Connected Service";
            this.View = new SinglePageView();
            this.View.DataContext = this;

            this.ServiceName = "Default Service Name";
            this.ExtraInformation = "Default Extra Information";
        }

        public string ServiceName
        {
            get { return this.serviceName; }
            set
            {
                this.serviceName = value;
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
                this.CalculateIsFinishEnabled();
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
                    this.CalculateAuthentication();
                }
                return this.authenticator;
            }
        }

        private void Authenticator_AuthenticationChanged(object sender, AuthenticationChangedEventArgs e)
        {
            this.CalculateAuthentication();
        }

        private void CalculateAuthentication()
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
            this.IsFinishEnabled = this.Authenticator.IsAuthenticated &&
                !string.IsNullOrEmpty(this.ServiceName) &&
                !string.IsNullOrEmpty(this.ExtraInformation);
        }

        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = this.ServiceName;
            instance.Metadata.Add("ExtraInfo", this.ExtraInformation);
            return Task.FromResult(instance);
        }
    }
}
