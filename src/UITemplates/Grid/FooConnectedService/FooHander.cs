using Microsoft.VisualStudio.ConnectedServices;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.Grid")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class FooHander : ConnectedServiceHandler
    {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");

            return new AddServiceInstanceResult("FooGrid", null);
        }
    }
}