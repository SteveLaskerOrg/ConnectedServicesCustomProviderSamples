using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectedServiceSample
{
    [Export(typeof(IConnectedServiceInstanceHandler))]
    [ExportMetadata("ProviderId", "ConnectedServiceSample.ConnectedServiceProvider")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("AppliesTo", "CSharp+Web")]
    internal class ConnectedServiceHandler : IConnectedServiceInstanceHandler
    {
        public async Task AddServiceInstanceAsync(IConnectedServiceInstanceContext context, CancellationToken ct)
        {
            await HandlerHelper.AddFileAsync(context, Utilities.GetResourceUri("SampleServiceTemplate.cs").ToString(), "SampleService.cs");

            await HandlerHelper.AddGettingStartedAsync(context, "Sample", new Uri("http://www.microsoft.com"));
        }
    }
}
