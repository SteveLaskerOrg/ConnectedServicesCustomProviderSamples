using Company.ConnectedServiceDemo.ViewModels;
using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Company.ConnectedServiceDemo
{
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Company.ConnectedServiceDemo.WizardDemoProvider")]
    internal class WizardDemoProvider : ConnectedServiceProvider
    {
        public WizardDemoProvider()
        {
            this.Category = "Sample";
            this.Name = "Sample Wizard Provider";
            this.Description = "Sample Provider with Wizard functionality.";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Resources/Wizard.jpg"));
            this.CreatedBy = "Contoso, Inc.";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples");
        }

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host)
        {
            ConnectedServiceConfigurator configurator = new WizardDemoWizardViewModel();
            return Task.FromResult(configurator);
        }
    }
}