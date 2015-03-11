using Microsoft.ConnectedServices.Samples.UpdateSupport.Views;
using Microsoft.VisualStudio.ConnectedServices;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.UpdateSupport.ViewModels
{
    internal class SinglePageViewModel : ConnectedServiceSinglePage
    {
        private ConnectedServiceProviderContext context;
        private string serviceName;
        private string extraInformation;
        private string authenticateMessage;
        private AuthenticatorViewModel authenticator;

        public SinglePageViewModel()
        {
            this.Title = "Samples";
            this.Description = "Updating a Connected Service";
            this.View = new SinglePageView();
            this.View.DataContext = this;

            this.ServiceName = "SampleService";
            this.ExtraInformation = "Default Extra Information";
        }

        /// <summary>
        /// Gets or sets the ConnectedServiceProviderContext that provides information about how the dialog was launched.
        /// </summary>
        public ConnectedServiceProviderContext Context
        {
            get { return this.context; }
            set
            {
                this.context = value;
                this.OnPropertyChanged();

                if (this.context != null && this.context.IsUpdating)
                {
                    this.ServiceName = this.context.UpdateContext.ServiceFolder.Text;
                    DesignerData designerData = this.context.GetExtendedDesignerData<DesignerData>();
                    if (designerData != null)
                    {
                        this.ExtraInformation = designerData.ExtraInformation;
                    }
                }
            }
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
                this.OnPropertyChanged();
                this.CalculateIsFinishEnabled();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the ServiceName property can be modified.
        /// </summary>
        public bool IsServiceNameEnabled
        {
            get
            {
                // disable editing of ServiceName if we are not logged in,
                // or an existing service is being updated
                return this.Authenticator.IsAuthenticated &&
                    (this.Context == null || !this.Context.IsUpdating);
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
                this.OnPropertyChanged();
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
                this.OnPropertyChanged();
            }
        }

        public AuthenticatorViewModel Authenticator
        {
            get
            {
                if (this.authenticator == null)
                {
                    this.authenticator = new AuthenticatorViewModel();
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
            this.OnPropertyChanged("IsServiceNameEnabled");
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
            // Pass the Service Name the user can enter to the Instance Name, 
            // used to specify the name of the folder under Service References
            instance.Name = this.ServiceName;
            // An example for how to pass additional info from the Configuration View to the Handler
            // Looking at the Templates\SampleServiceTemplate.cs you'll notice $ServiceInstance.ExtraInfo$ token
            // HandlerHelper.AddFileAsync() parses these properties for token replacement
            instance.Metadata.Add("ExtraInfo", this.ExtraInformation);

            // store some extra designer data, so it can be read back during update
            if (this.Context != null)
            {
                this.Context.SetExtendedDesignerData(new DesignerData() { ExtraInformation = this.ExtraInformation });
            }

            return Task.FromResult(instance);
        }
    }
}