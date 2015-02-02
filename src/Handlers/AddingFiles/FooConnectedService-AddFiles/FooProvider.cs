using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Reflection;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace Microsoft.ConnectedServices.Samples
{
    [Export(typeof(ConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.AddingFiles")]
    internal class FooProvider : ConnectedServiceProvider
    {
        public FooProvider()
        {
            this.Name = "Sample: Adding Files";
            this.Category = "Foo";
            this.Description = "A sample handler demonstrating Adding Files to the project";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + Assembly.GetExecutingAssembly().ToString() + ";component/" + "Resources/Icon.png"));
            this.CreatedBy = "Microsoft";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("http://Microsoft.com");
        }

        public override IEnumerable<Tuple<string, Uri>> GetSupportedTechnologyLinks()
        {
            // A list of supported technolgoies, such as which services it supports
            yield return Tuple.Create("Azure Active Directory", new Uri("http://azure.microsoft.com/en-us/services/active-directory/"));
        }
        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderHost host)
        {
            Views.FooSinglePageView view = new Views.FooSinglePageView();
            ViewModels.FooSinglePageViewModel vm = (ViewModels.FooSinglePageViewModel)view.DataContext;
            vm.View = view;
            return Task.FromResult<ConnectedServiceConfigurator>(vm);
        }

    }
}
