using Microsoft.VisualStudio.ConnectedServices;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo
{
    [Export(typeof(IConnectedServiceInstanceHandler))]
    [ExportMetadata("ProviderId", "Microsoft.VisualStudio.ConnectedServices.Sample.WizardProvider")]
    [ExportMetadata("Version", "1.0")]
    [ExportMetadata("AppliesTo", "CSharp | VB")]
    internal class WizardDemoCSharpHandler : IConnectedServiceInstanceHandler
    {
        public Task AddServiceInstanceAsync(IConnectedServiceInstanceContext context, System.Threading.CancellationToken ct)
        {
            context.Logger.WriteMessage(LoggerMessageCategory.Information, "WizardHandler invoked.");

            return Task.FromResult(true);
        }
    }

}
