using Microsoft.VisualStudio.ConnectedServices;

namespace Microsoft.ConnectedServices.Samples.Handlers.UpdateXmlConfig
{
    internal class Instance : ConnectedServiceInstance
    {

        public Instance()
        {
            this.InstanceId = "Contoso Service";
            this.Name = "Contoso Service";
            this.ConfigOptions = new Models.ConfigOptions();
        }

        public Models.ConfigOptions ConfigOptions { get; set; }
    }
}
