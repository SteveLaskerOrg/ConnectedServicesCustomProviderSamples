using Microsoft.VisualStudio.ConnectedServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples
{
    [ConnectedServiceHandlerExport(
        "Microsoft.ConnectedServiceSamples.FooService.Wizard",
        AppliesTo = "CSharp")]
    internal class FooHander : ConnectedServiceHandler
    {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");

            return new AddServiceInstanceResult("FooWizard", null);
        }
    }
}