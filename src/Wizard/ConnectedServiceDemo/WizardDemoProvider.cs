using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Company.ConnectedServiceDemo
{
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Company.ConnectedServiceDemo.WizardDemoProvider")]
    internal class WizardDemoProvider : ConnectedServiceProvider
    {
        public WizardDemoProvider()
        {
        }

        public override string Name
        {
            get { return "Sample Wizard Provider"; }
        }

        public override string Category
        {
            get { return "Sample"; }
        }

        public override string Description
        {
            get { return "Sample Provider with Wizard functionality."; }
        }

        public override ImageSource Icon
        {
            get
            {
                return new BitmapImage(Utilities.GetResourceUri("Wizard.jpg"));
            }
        }

        public override string CreatedBy
        {
            get { return "Contoso, Inc."; }
        }

        public override Version Version
        {
            get { return new Version(1, 0, 0); }
        }

        public override Uri MoreInfoUri
        {
            get { return new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"); }
        }

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host)
        {
            ConnectedServiceConfigurator configurator = new WizardDemoProviderWizard();
            return Task.FromResult(configurator);
        }
    }
}