using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Reflection;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Microsoft.ConnectedServices.Samples {
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.Wizard")]
    internal class FooProvider : ConnectedServiceProvider {
        public FooProvider() {
            this.Name = "Sample: Wizard Template";
            this.Category = "Foo";
            this.Description = "A sample provider demonstrating the Wizard UI template";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + Assembly.GetExecutingAssembly().ToString() + ";component/" + "Resources/Icon.png"));
            this.CreatedBy = "Microsoft";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("http://Microsoft.com");
        }

        public override IEnumerable<Tuple<string, Uri>> GetSupportedTechnologyLinks() {
            // A list of supported technolgoies, such as which services it supports
            // If a Provider configured Dynamics CRM with Azure Redis Cache and Azure Auth, 
            // it would include the following
            yield return Tuple.Create("Dynamics CRM", new Uri("http://www.microsoft.com/en-us/dynamics/crm.aspx"));
            yield return Tuple.Create("Azure Redis Cache", new Uri("http://azure.microsoft.com/en-us/services/cache/"));
            yield return Tuple.Create("Azure Active Directory", new Uri("http://azure.microsoft.com/en-us/services/active-directory/"));
        }
        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host) {
            return Task.FromResult<ConnectedServiceConfigurator>(new ViewModels.FooServiceWizard());
        }
    }
}