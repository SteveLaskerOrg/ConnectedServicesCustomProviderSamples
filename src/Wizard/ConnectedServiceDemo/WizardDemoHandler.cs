using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo
{
    [Export(typeof(ConnectedServiceHandler))]
    [ExportMetadata("ProviderId", "Company.ConnectedServiceDemo.WizardDemoProvider")]
    [ExportMetadata("AppliesTo", "CSharp")]
    internal class WizardDemoHandler : ConnectedServiceHandler
    {
        public override async Task AddServiceInstanceAsync(ConnectedServiceInstanceContext context, CancellationToken ct)
        {
            string texts = string.Join(Environment.NewLine, 
                context.ServiceInstance.Metadata
                    .Where(pair => pair.Key.StartsWith("Page"))
                    .Select(pair => pair.Key + ": " + pair.Value));

            // writes a message to the progress dialog and the output window
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "WizardHandler invoked.  Texts entered: " + Environment.NewLine + texts);

            // Generate a code file into the user's project from a template.
            // The tokens in the template will be replaced by the HandlerHelper.
            string templateResourceUri = "pack://application:,,/" + this.GetType().Assembly.ToString() + ";component/Resources/SampleServiceTemplate.cs";
            await HandlerHelper.AddFileAsync(context, templateResourceUri, "SampleWizardService.cs");

            // Adds the 'Getting Started' artifact to the project in the "SampleWizard" directory and opens the page
            await HandlerHelper.AddGettingStartedAsync(context, "SampleWizard", new Uri("https://github.com/SteveLasker/ConnectedServicesCustomProviderSamples"));
        }
    }
}
