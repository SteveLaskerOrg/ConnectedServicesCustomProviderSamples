using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ConnectedServices;
using System.ComponentModel.Composition;
using System.Threading;

namespace Microsoft.ConnectedServices.Samples
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.AddingFiles")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class FooHander : ConnectedServiceHandler
    {
        public override async Task AddServiceInstanceAsync(ConnectedServiceInstanceContext context, CancellationToken ct)
        {
            // Generate a code file into the user's project from a template.
            // The tokens in the template will be replaced by the HandlerHelper.
            // Place service specific scaffolded code under the service folder
            string templateResourceUri = "pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Templates/SampleServiceTemplate.cs";
            string serviceFolder = string.Format("Service References\\{0}\\", context.ServiceInstance.Name);
            await HandlerHelper.AddFileAsync(context, templateResourceUri, serviceFolder + "SampleSinglePage.cs");

            // Adds the 'Getting Started' artifact to the project in the "SampleSinglePage" directory and opens the page
            // This would be your guidance on how a developer would complete development for the service
            // What Happened, and required Next Steps, and Sample code
            await HandlerHelper.AddGettingStartedAsync(context, context.ServiceInstance.Name, new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
