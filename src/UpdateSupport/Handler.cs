using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.UpdateSupport
{
    [ConnectedServiceHandlerExport(
        "Microsoft.Samples.UpdateSupport",
        AppliesTo = "CSharp")]
    internal class Handler : ConnectedServiceHandler
    {
        private const string GettingStartedUrl = "https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples";

        /// <summary>
        /// Called to add a new Connected Service to the project.
        /// </summary>
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            await this.GenerateCodeFiles(context);

            // Adds the 'ConnectedService.json' and 'Getting Started' artifacts to the project in the "SampleSinglePage" directory and opens the page
            // This would be your guidance on how a developer would complete development for the service
            // What Happened, and required Next Steps, and Sample code
            return new AddServiceInstanceResult(context.ServiceInstance.Name, new Uri(Handler.GettingStartedUrl));
        }

        /// <summary>
        /// Called to update an existing Connected Service to the project.
        /// </summary>
        public override async Task<UpdateServiceInstanceResult> UpdateServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            await this.GenerateCodeFiles(context);

            return new UpdateServiceInstanceResult()
            {
                // ensure the GettingStartedUrl is up to date
                GettingStartedUrl = new Uri(Handler.GettingStartedUrl)
            };
        }

        private async Task GenerateCodeFiles(ConnectedServiceHandlerContext context)
        {
            string templateResourceUri = "pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Templates/SampleServiceTemplate.cs";

            // Generate a code file into the user's project from a template.
            // The tokens in the template will be replaced by the HandlerHelper.
            // Place service specific scaffolded code under the service folder

            string serviceFolder;
            if (context.IsUpdating)
            {
                serviceFolder = context.UpdateContext.ServiceFolder.CanonicalName;
            }
            else
            {
                serviceFolder = Path.Combine(context.HandlerHelper.GetServiceArtifactsRootFolder(), context.ServiceInstance.Name);
            }

            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Generating SampleSinglePage.cs code in '{0}'...", serviceFolder);

            await context.HandlerHelper.AddFileAsync(templateResourceUri, Path.Combine(serviceFolder, "SampleSinglePage.cs"));
        }
    }
}
