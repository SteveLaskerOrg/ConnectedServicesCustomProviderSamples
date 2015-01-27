using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples {
    internal class FooConnectedServiceInstance : ConnectedServiceInstance {

        public FooConnectedServiceInstance() {
            this.InstanceId = "FooService";
            this.Name = Resources.ConnectedServiceInstance_Name;
            this.ConfigOptions = new Models.ConfigOptions();
        }

        public Models.ConfigOptions ConfigOptions { get; set; }
    }
}
