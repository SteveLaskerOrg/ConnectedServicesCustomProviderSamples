using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ConnectedServiceSample
{
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "ConnectedServiceSample.Provider")]
    internal class Provider : ConnectedServiceProvider
    {
        public Provider()
        {
            this.Category = "Sample";
            this.Name = "Sample Grid Provider";
            this.Description = "Sample Provider with Grid functionality.";
            this.Icon = new BitmapImage(Utilities.GetResourceUri("Image.png"));
            this.CreatedBy = "Contoso, Inc.";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples");
        }

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host)
        {
            ConnectedServiceConfigurator configurator = new Grid();
            return Task.FromResult(configurator);
        }
    }
}
