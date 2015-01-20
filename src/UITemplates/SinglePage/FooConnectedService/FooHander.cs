using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ConnectedServices;
using System.ComponentModel.Composition;
using System.Threading;

namespace Microsoft.ConnectedServices.Samples
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServiceSamples.FooService.SinglePage")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class FooHander : ConnectedServiceHandler
    {
        public override async Task AddServiceInstanceAsync(ConnectedServiceInstanceContext context, CancellationToken ct)
        {
            // See Handler Samples for how to work with the project system 
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");
        }
    }
}
