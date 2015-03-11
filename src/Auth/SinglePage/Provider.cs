﻿using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Microsoft.ConnectedServices.Samples.Authentication.SinglePage
{
    [ConnectedServiceProviderExport("Microsoft.Samples.SinglePageAuth")]
    internal class Provider : ConnectedServiceProvider
    {
        public Provider()
        {
            // Set the values to be displayed in the first Connected Services Selection UI
            this.Category = "Foo";
            this.Name = "Sample: Single Page Auth";
            this.Description = "Sample Provider with Single Page Auth functionality.";
            this.Icon = new BitmapImage(new Uri("pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Resources/Icon.png"));
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
            ConnectedServiceConfigurator configurator = new ViewModels.SinglePageViewModel();
            return Task.FromResult(configurator);
        }
    }
}
