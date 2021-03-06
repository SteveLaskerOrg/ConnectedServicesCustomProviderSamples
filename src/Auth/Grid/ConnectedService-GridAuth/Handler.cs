﻿using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.ConnectedServices.Samples.FooService.GridAuth")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class Handler : ConnectedServiceHandler
    {
        /// <summary>
        /// AddServiceInstanceAsync is responsible for adding any artifacts to the project that will be used
        /// to connect to the service.
        /// </summary>
        public override async Task AddServiceInstanceAsync(ConnectedServiceInstanceContext context, CancellationToken ct)
        {
            // See Handler Samples for how to work with the project system 
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");
        }
    }
}
