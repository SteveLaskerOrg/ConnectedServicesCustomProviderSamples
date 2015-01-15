using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectedServiceSample
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "ConnectedServiceSample.Provider")]
    [ExportMetadata("AppliesTo", "CSharp+Web")]
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
            string templateResourceUri = "pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Resources/SampleServiceTemplate.cs";
            await HandlerHelper.AddFileAsync(context, templateResourceUri, "SampleGridService.cs");

            // Adds the 'Getting Started' artifact to the project in the "SampleGrid" directory and opens the page
            await HandlerHelper.AddGettingStartedAsync(context, "SampleGrid", new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
