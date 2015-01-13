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
            await HandlerHelper.AddFileAsync(context, Utilities.GetResourceUri("SampleServiceTemplate.cs").ToString(), "SampleSinglePage.cs");

            await HandlerHelper.AddGettingStartedAsync(context, "SampleSinglePage", new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
