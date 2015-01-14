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
        public override async Task AddServiceInstanceAsync(ConnectedServiceInstanceContext context, CancellationToken ct)
        {
            string templateResourceUri = "pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Resources/SampleServiceTemplate.cs";
            await HandlerHelper.AddFileAsync(context, templateResourceUri, "SampleSinglePage.cs");

            await HandlerHelper.AddGettingStartedAsync(context, "SampleSinglePage", new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
