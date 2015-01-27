using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ConnectedServices;
using Microsoft.ConnectedServices.Samples;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.ConnectedServices.Samples.ViewModels {
    internal class FooSinglePageViewModel : ConnectedServiceSinglePage {
        public FooSinglePageViewModel() {
            //if (!(bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue)) {
            this.View = new Views.FooSinglePageView();
            this.View.DataContext = this;
            //}
            this.Title = "Title: Single Page Config";
            this.Description = "Description: Configure the Foo Service";
            this.IsFinishEnabled = true;
            this.RedirectUrl = "http://MyCompanyLoginUrl.dot";
        }

        private string _redirectUrl;
        public string RedirectUrl {
            get { return _redirectUrl; }
            set {
                if (value != _redirectUrl) {
                    _redirectUrl = value;
                    this.OnNotifyPropertyChanged("RedirectUrl");
                }
            }
        }

        public override Task<ConnectedServiceAuthenticator> CreateAuthenticatorAsync() {
            // Add Authentication to the Auth UI Block
            // See the Auth Samples for additional info
            return Task.FromResult<ConnectedServiceAuthenticator>(new AuthenticatorViewModel());
        }

        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync() {
            // Construct an instance, specific to this service
            // Used to more easily pass information to the various handlers, 
            // without having to use the instance.Metadata property bag
            FooConnectedServiceInstance instance = new FooConnectedServiceInstance();

            // Pass some specific values over, which may be generated as a result
            // of creating an OAuth endopint on the service
            instance.ConfigOptions.ConsumerKey = Guid.NewGuid().ToString();
            instance.ConfigOptions.ConsumerSecret = Guid.NewGuid().ToString();

            // Pass a value the user entered directly
            instance.ConfigOptions.RedirectUrl = this.RedirectUrl;

            return Task.FromResult<ConnectedServiceInstance>(instance);
        }
    }
}