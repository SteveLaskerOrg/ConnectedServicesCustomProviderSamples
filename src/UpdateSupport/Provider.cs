﻿using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Microsoft.ConnectedServices.Samples.UpdateSupport
{
    [ConnectedServiceProviderExport("Microsoft.Samples.UpdateSupport", SupportsUpdate = true)]
    internal class Provider : ConnectedServiceProvider
    {
        public Provider()
        {
            this.Name = "Sample: Update Support";
            this.Category = "Foo";
            this.Description = "A sample handler demonstrating supporting update of a service";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + Assembly.GetExecutingAssembly().ToString() + ";component/" + "Resources/Icon.png"));
            this.CreatedBy = "Microsoft";
            this.Version = new Version(1, 0, 0);
            this.MoreInfoUri = new Uri("http://Microsoft.com");
        }

        public override Task<ConnectedServiceConfigurator> CreateConfiguratorAsync(ConnectedServiceProviderContext context)
        {
            // To get Designtime binding, setting the DataContext in XAML, we need to order the creation of objects properly
            // Not ordering them can create a stack overflow of views instancing viewmodels

            // First create the View we'll use
            Views.SinglePageView view = new Views.SinglePageView();
            // Grab the datacontext set in XAML 
            ViewModels.SinglePageViewModel vm = (ViewModels.SinglePageViewModel)view.DataContext;

            // Close the loop
            vm.View = view;
            vm.Context = context;

            return Task.FromResult<ConnectedServiceConfigurator>(vm);
        }
    }
}