using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.Authentication.SinglePage.ViewModels
{
    internal class SinglePageViewModel : ConnectedServiceSinglePage
    {
        public SinglePageViewModel()
        {
            this.Title = "Foo Service";
            this.Description = "Sample SinglePage with Auth";
            this.View = new Views.SinglePageView();
            this.View.DataContext = this;

            this.ServiceName = "SampleService";
        }

        private string _serviceName;
        /// <summary>
        /// Gets or sets the first value that the user can enter.
        /// </summary>
        public string ServiceName
        {
            get { return this._serviceName; }
            set
            {
                this._serviceName = value;
                this.OnPropertyChanged();
                this.CalculateIsFinishEnabled();
            }
        }


        private string _authenticateMessage;
        /// <summary>
        /// Gets or sets the message shown to the user when he is not signed in.
        /// </summary>
        public string AuthenticateMessage
        {
            get { return this._authenticateMessage; }
            set
            {
                this._authenticateMessage = value;
                this.OnPropertyChanged();
            }
        }

        private AuthenticatorViewModel _authenticator;
        // Hosts the Authentication ViewModel
        public AuthenticatorViewModel Authenticator
        {
            get
            {
                if (this._authenticator == null)
                {
                    this._authenticator = new AuthenticatorViewModel();
                    this._authenticator.AuthenticationChanged += Authenticator_AuthenticationChanged;
                    this.CalculateAuthentication();
                }
                return this._authenticator;
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
            // basic example for toggling the state of the Add/Update/Finish button
            this.IsFinishEnabled = this.Authenticator.IsAuthenticated &&
                !string.IsNullOrEmpty(this.ServiceName);
        }

        /// <summary>
        /// This method is called when the user finishes configuring the service.
        /// It returns the 'finished' ConnectedServiceInstance that will be passed to the Handler.
        /// </summary>
        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            return Task.FromResult(new ConnectedServiceInstance());
        }
    }
}
