using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConnectedServiceSample
{
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "ConnectedServiceSample.ConnectedServiceProvider")]
    internal class Provider : ConnectedServiceProvider
    {
        public override string Name { get { return "Sample Grid Provider"; } }
        public override string Category { get { return "Sample"; } }
        public override string CreatedBy { get { return "Contoso, Inc."; } }
        public override string Description { get { return "Sample Provider with Grid functionality."; } }
        public override Version Version { get { return new Version(1, 0, 0); } }
        public override Uri MoreInfoUri { get { return new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"); } }
        public override ImageSource Icon
        {
            get
            {
                return new BitmapImage(Utilities.GetResourceUri("Image.png"));
            }
        }

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host)
        {
            ConnectedServiceConfigurator configurator = new Grid();
            return Task.FromResult(configurator);
        }
    }
}
