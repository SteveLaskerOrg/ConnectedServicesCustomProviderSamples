﻿using Microsoft.VisualStudio.ConnectedServices;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.Samples.ConnectedServices.UITemplates.Wizard
{
    [ConnectedServiceHandlerExport(
        "Contoso.Samples.WizardUITemplate",
        AppliesTo = "CSharp")]
    internal class Handler : ConnectedServiceHandler
    {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");

            return new AddServiceInstanceResult("SampleServiceWizardUITemplate", null);
        }
    }
}