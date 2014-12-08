using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ConnectedServiceSample
{
    [Export(typeof(IConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "ConnectedServiceSample.ConnectedServiceProvider")]
    internal class ConnectedServiceProvider : IConnectedServiceProvider
    {
        public string Name { get { return "Sample Grid Provider"; } }
        public string Category { get { return "Sample"; } }
        public string CreatedBy { get { return "Contoso, Inc."; } }
        public string Description { get { return "Sample Provider with Grid functionality."; } }
        public Version Version { get { return new Version(1, 0, 0); } }
        public Uri MoreInfoUri { get { return new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"); } }
        public ImageSource Icon
        {
            get
            {
                return new BitmapImage(Utilities.GetResourceUri("Image.png"));
            }
        }

        internal ConnectedServiceProviderGrid CurrentProviderGrid { get; set; }

        public Task<object> CreateService(Type serviceType, IServiceProvider serviceProvider)
        {
            object service = this.CurrentProviderGrid;
            if (service == null)
            {
                // The ConnectedServiceProviderGrid provides all supported services

                // cache the grid provider instance, so the same instance is used for all the different service types
                this.CurrentProviderGrid = new ConnectedServiceProviderGrid(this);
                service = this.CurrentProviderGrid;
            }

            return Task.FromResult(service);
        }
    }
}
