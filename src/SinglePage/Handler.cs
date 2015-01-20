using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectedServiceSinglePageSample
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "ConnectedServiceSinglePageSample.Provider")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class Handler : ConnectedServiceHandler
    {
        /// <summary>
        /// AddServiceInstanceAsync is responsible for adding any artifacts to the project that will be used
        /// to connect to the service.
        /// </summary>
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
