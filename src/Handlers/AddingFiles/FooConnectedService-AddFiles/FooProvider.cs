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

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderContext context)
        {
            // To get Designtime binding, setting the DataContext in XAML, we need to order the creation of objects properly
            // Not ordering them can create a stack overflow of views instancing viewmodels
            
            // First create the View we'll use
            Views.FooSinglePageView view = new Views.FooSinglePageView();
            // Grab the datacontext set in XAML 
            ViewModels.FooSinglePageViewModel vm = (ViewModels.FooSinglePageViewModel)view.DataContext;

            // Close the loop
            vm.View = view;
            return Task.FromResult<ConnectedServiceConfigurator>(vm);
        }
    }
}
