using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Company.ConnectedServiceDemo
{
    [Export(typeof(IConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Microsoft.VisualStudio.ConnectedServices.Sample.WizardProvider")]
    [ExportMetadata("Version", "1.0")]
    internal class WizardDemoProvider : NotifyPropertyChangeBase, IConnectedServiceProvider
    {
        public WizardDemoProvider()
        {
        }

        public string Name
        {
            get { return "Sample Wizard Provider"; }
        }

        public string Category
        {
            get { return "Sample"; }
        }

        public string Description
        {
            get { return "Sample Provider with Wizard functionality."; }
        }

        public ImageSource Icon
        {
            get
            {
                return new BitmapImage(Utilities.GetResourceUri("Wizard.jpg"));
            }
        }

        public string CreatedBy
        {
            get { return "Steve Lasker"; }
        }

        public Uri MoreInfoUri
        {
            get { return new Uri("https://github.com/SteveLasker/ConnectedServiceProviderSample"); }
        }

        public Task<object> CreateService(Type serviceType, IServiceProvider serviceProvider)
        {
            object service = null;

            if (serviceType == typeof(IConnectedServiceProviderUI))
            {
                service = new WizardDemoProviderWizard();
            }

            return Task.FromResult(service);
        }
    }
}