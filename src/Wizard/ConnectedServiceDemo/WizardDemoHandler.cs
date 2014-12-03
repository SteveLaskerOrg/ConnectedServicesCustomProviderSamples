using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo
{
    [Export(typeof(IConnectedServiceInstanceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.VisualStudio.ConnectedServices.Sample.WizardProvider")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("AppliesTo", "CSharp | VB")]
    internal class WizardDemoHandler : IConnectedServiceInstanceHandler
    {
        public async Task AddServiceInstanceAsync(IConnectedServiceInstanceContext context, CancellationToken ct)
        {
            string texts = string.Join(Environment.NewLine, 
                context.ServiceInstance.Metadata
                    .Where(pair => pair.Key.StartsWith("Page"))
                    .Select(pair => pair.Key + ": " + pair.Value));

            context.Logger.WriteMessage(LoggerMessageCategory.Information, "WizardHandler invoked.  Texts entered: " + Environment.NewLine + texts);

            await HandlerHelper.AddFileAsync(context, Utilities.GetResourceUri("SampleServiceTemplate.cs").ToString(), "SampleWizardService.cs");

            await HandlerHelper.AddGettingStartedAsync(context, "SampleWizard", new Uri("https://github.com/SteveLasker/ConnectedServiceProviderSample"));
        }
    }
}
