using Microsoft.VisualStudio.ConnectedServices;
using System.Collections.Generic;

namespace ConnectedServiceSample
{
    internal class ConnectedServiceInstance : IConnectedServiceInstance
    {
        private Dictionary<string, object> metadata = new Dictionary<string, object>();

        public ConnectedServiceInstance(string name, string column1, string detail1, string detail2)
        {
            this.Name = name;
            this.metadata.Add("Column1", column1);
            this.metadata.Add("Detail1", detail1);
            this.metadata.Add("Detail2", detail2);
        }

        public string Name { get; set; }
        public string InstanceId { get { return this.Name; } }
        public string ProviderId { get { return "ConnectedServiceSample.ConnectedServiceProvider"; } }

        public IReadOnlyDictionary<string, object> Metadata { get { return this.metadata; } }

        internal void SetColumn1(string value)
        {
            this.metadata["Column1"] = value;
        }
    }
}
