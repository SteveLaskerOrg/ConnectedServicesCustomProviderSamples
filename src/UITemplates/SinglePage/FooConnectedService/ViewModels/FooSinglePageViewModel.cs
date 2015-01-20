using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ConnectedServices;
using Microsoft.ConnectedServices.Samples;
namespace Microsoft.ConnectedServices.Samples.ViewModels
{
    internal class FooSinglePageViewModel : ConnectedServiceSinglePage
    {
        public FooSinglePageViewModel()
        {

            this.View = new Views.FooSinglePageView();
            this.Title = "Title: Single Page Config";
            this.Description = "Description: Configure the Foo Service";
            this.IsFinishEnabled = true;
        }

        public override Task<ConnectedServiceAuthenticator> CreateAuthenticatorAsync()
        {
            // Add Authentication to the Auth UI Block
            // See the Auth Samples for additional info
            return Task.FromResult<ConnectedServiceAuthenticator>(new AuthenticatorViewModel());
        }

        public override Task<ConnectedServiceInstance> GetFinishedServiceInstanceAsync()
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = "Foo Service";
            return Task.FromResult(instance);
        }
    }
}