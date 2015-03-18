﻿using Microsoft.VisualStudio.ConnectedServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.UITemplates.SinglePage
{
    [ConnectedServiceHandlerExport(
        "Microsoft.Samples.SinglePageUITemplate",
        AppliesTo = "CSharp")]
    internal class Handler : ConnectedServiceHandler
    {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            // See Handler Samples for how to work with the project system 
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");

            return new AddServiceInstanceResult("SampleServiceSinglePageUITemplate", null);
        }
    }
}
