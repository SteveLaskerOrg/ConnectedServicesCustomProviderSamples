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
    [ExportMetadata("Version", "1.0")]
    internal class ConnectedServiceProvider : IConnectedServiceProvider
    {
        public string Name { get { return "Sample"; } }
        public string Category { get { return "Sample"; } }
        public string CreatedBy { get { return "Contoso, Inc."; } }
        public string Description { get { return "A sample Connected Service"; ; } }
        public Uri MoreInfoUri { get { return new Uri("http://www.microsoft.com"); } }
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
                // cache the grid provider instance, so the same instance is used for all the different service types
                this.CurrentProviderGrid = new ConnectedServiceProviderGrid(this);
                service = this.CurrentProviderGrid;
            }

            return Task.FromResult(service);
        }
    }
}
