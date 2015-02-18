using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ConnectedServices;
using System.ComponentModel.Composition;
using System.Threading;

namespace Microsoft.ConnectedServices.Samples {
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.Wizard")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class FooHander : ConnectedServiceHandler {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct) {
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");

            return new AddServiceInstanceResult("FooWizard", null);
        }
    }
}