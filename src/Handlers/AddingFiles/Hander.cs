using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.Handlers.AddingFiles
{
    [ConnectedServiceHandlerExport(
        "Microsoft.Samples.AddingFiles",
        AppliesTo = "CSharp")]
    internal class Hander : ConnectedServiceHandler
    {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            // Generate a code file into the user's project from a template.
            // The tokens in the template will be replaced by the HandlerHelper.
            // Place service specific scaffolded code under the service folder
            string templateResourceUri = "pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Templates/SampleServiceTemplate.cs";
            string serviceFolder = Path.Combine(context.HandlerHelper.GetServiceArtifactsRootFolder(), context.ServiceInstance.Name);
            await context.HandlerHelper.AddFileAsync(templateResourceUri, serviceFolder + "SampleSinglePage.cs");

            // Adds the 'ConnectedService.json' and 'Getting Started' artifacts to the project in the "SampleSinglePage" directory and opens the page
            // This would be your guidance on how a developer would complete development for the service
            // What Happened, and required Next Steps, and Sample code
            return new AddServiceInstanceResult(context.ServiceInstance.Name, new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
