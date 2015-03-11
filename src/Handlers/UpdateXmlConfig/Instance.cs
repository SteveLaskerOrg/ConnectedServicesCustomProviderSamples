using Microsoft.VisualStudio.ConnectedServices;

namespace Microsoft.ConnectedServices.Samples.Handlers.UpdateXmlConfig
{
    internal class Instance : ConnectedServiceInstance
    {

        public Instance()
        {
            this.InstanceId = "FooService";
            this.Name = Resources.ConnectedServiceInstance_Name;
            this.ConfigOptions = new Models.ConfigOptions();
        }

        public Models.ConfigOptions ConfigOptions { get; set; }
    }
}
