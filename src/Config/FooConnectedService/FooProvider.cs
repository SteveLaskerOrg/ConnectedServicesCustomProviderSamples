using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Reflection;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Microsoft.ConnectedServices.Samples {
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.Config")]
    internal class FooProvider : ConnectedServiceProvider {
        public FooProvider() {
            this.Name = "Sample: Config Management";
            this.Category = "Foo";
            this.Description = "A sample provider demonstrating config management";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + Assembly.GetExecutingAssembly().ToString() + ";component/" + "Resources/Icon.png"));
            this.CreatedBy = "Microsoft";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("http://Microsoft.com");
        }

        public override IEnumerable<Tuple<string, Uri>> GetSupportedTechnologyLinks() {
            yield return Tuple.Create("Azure Redis Cache", new Uri("http://azure.microsoft.com/en-us/services/cache/"));
        }
        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host) {
            return Task.FromResult<ConnectedServiceConfigurator>(new ViewModels.FooSinglePageViewModel());
        }
    }
}