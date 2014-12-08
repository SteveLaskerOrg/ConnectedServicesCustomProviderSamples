using Microsoft.VisualStudio.ConnectedServices;
using System.Collections.Generic;
using System.Diagnostics;

namespace Company.ConnectedServiceDemo
{
    internal class WizardDemoInstance : NotifyPropertyChangeBase, IConnectedServiceInstance
    {
        public const string Field1Name = "Field1";
        public const string Field2Name = "Field2";

        private string name;

        public WizardDemoInstance(string name, string instanceId, string providerId, params string[] metadataKeysAndValues)
        {
            this.Name = name;
            this.InstanceId = instanceId;
            this.ProviderId = providerId;

            var metadata = new Dictionary<string, object>();

            if (metadataKeysAndValues == null || metadataKeysAndValues.Length == 0)
            {
                metadataKeysAndValues = new string[] { WizardDemoInstance.Field1Name, "some value", WizardDemoInstance.Field2Name, "another value" };
            }

            // Take metadataKeysAndValues in pairs...
            Debug.Assert((metadataKeysAndValues.Length % 2) == 0);

            for (int i = 0; i < metadataKeysAndValues.Length; i += 2)
            {
                metadata.Add(metadataKeysAndValues[i], metadataKeysAndValues[i + 1]);
            }

            this.Metadata = metadata;
        }

        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        public string InstanceId { get; private set; }

        public string ProviderId { get; private set; }

        public IReadOnlyDictionary<string, object> Metadata { get; private set; }
    }
}