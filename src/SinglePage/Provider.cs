using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ConnectedServiceSinglePageSample
{
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "ConnectedServiceSinglePageSample.Provider")]
    internal class Provider : ConnectedServiceProvider
    {
        public Provider()
        {
            this.Category = "Sample";
            this.Name = "Sample Single Page Provider";
            this.Description = "Sample Provider with Single Page functionality.";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Resources/Icon.png"));
            this.CreatedBy = "Contoso, Inc.";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples");
        }

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host)
        {
            ConnectedServiceConfigurator configurator = new SinglePageViewModel();
            return Task.FromResult(configurator);
        }
    }
}
