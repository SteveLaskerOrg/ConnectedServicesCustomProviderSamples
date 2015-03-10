using Microsoft.VisualStudio.ConnectedServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples
{
    [ConnectedServiceHandlerExport(
        "Microsoft.ConnectedServiceSamples.FooService.Config",
        AppliesTo = "CSharp")]
    internal class FooHander : ConnectedServiceHandler
    {
        public override async Task<AddServiceInstanceResult> AddServiceInstanceAsync(ConnectedServiceHandlerContext context, CancellationToken ct)
        {
            // See Handler Samples for how to work with the project system 
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Handler Invoked");
            await UpdateConfigFileAsync(context);

            return new AddServiceInstanceResult("FooConfig", null);
        }

        private static async Task UpdateConfigFileAsync(ConnectedServiceHandlerContext context)
        {
            // Push an update to the progress notifications
            // Introduce Resources as the means to manage strings shown to users, which may get localized
            // Or, at least verified by someone that should be viewing strings, not buried in the code
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, Resources.LogMessage_UpdatingConfigFile);
            // Now that we're passing more elaborate values between the provider and the handler
            // We'll start using a specific Instance so we can get stronger type verification
            FooConnectedServiceInstance fooInstance = (FooConnectedServiceInstance)context.ServiceInstance;

            // Launch the EditableConfigHelper to write several entries to the Config file
            using (EditableXmlConfigHelper configHelper = context.CreateEditableXmlConfigHelper())
            {
                // We ahve the option to write name/value pairs
                configHelper.SetAppSetting("ConsumerKey",
                    fooInstance.ConfigOptions.ConsumerKey,
                    "Heading on the first entry to identify the block of settings");
                configHelper.SetAppSetting("ConsumerSecret",
                    fooInstance.ConfigOptions.ConsumerSecret,
                    "Second Comment");
                configHelper.SetAppSetting("RedirectUrl",
                    fooInstance.ConfigOptions.RedirectUrl);
                // no comment on the third

                // Write the values to disk
                configHelper.Save();
            }
         
            // Some updates to the progress dialog
            System.Threading.Thread.Sleep(1000);
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Doing Something Else");
            System.Threading.Thread.Sleep(1000);
            await context.Logger.WriteMessageAsync(LoggerMessageCategory.Information, "Another Entry to show progress");
            System.Threading.Thread.Sleep(1000);
        }
    }
}