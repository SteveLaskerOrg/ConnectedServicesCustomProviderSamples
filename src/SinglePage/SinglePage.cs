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

        /// <summary>
        /// Gets or sets the first value that the user can enter.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the second value that the user can enter.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the message shown to the user when he is not signed in.
        /// </summary>
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

        /// <summary>
        /// The event handler that is called when the user signs in and out.
        /// </summary>
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

        /// <summary>
        /// Creates the ConnectedServiceAuthenticator object, which controls whether the user is signed in.
        /// </summary>
        public override Task<ConnectedServiceAuthenticator> CreateAuthenticatorAsync()
        {
            return Task.FromResult<ConnectedServiceAuthenticator>(this.Authenticator);
        }

        /// <summary>
        /// The logic that sets whether the user can finish configuring the service.
        /// </summary>
        private void CalculateIsFinishEnabled()
        {
            this.IsFinishEnabled = this.Authenticator.IsAuthenticated &&
                !string.IsNullOrEmpty(this.ServiceName) &&
                !string.IsNullOrEmpty(this.ExtraInformation);
        }

        /// <summary>
        /// This method is called when the user finishes configuring the service.
        /// It returns the 'finished' ConnectedServiceInstance that will be passed to the Handler.
        /// </summary>
        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = this.ServiceName;
            instance.Metadata.Add("ExtraInfo", this.ExtraInformation);
            return Task.FromResult(instance);
        }
    }
}
