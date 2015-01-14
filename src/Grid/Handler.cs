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
        public override async Task AddServiceInstanceAsync(ConnectedServiceInstanceContext context, CancellationToken ct)
        {
            await HandlerHelper.AddFileAsync(context, Utilities.GetResourceUri("SampleServiceTemplate.cs").ToString(), "SampleService.cs");

            await HandlerHelper.AddGettingStartedAsync(context, "Sample", new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
