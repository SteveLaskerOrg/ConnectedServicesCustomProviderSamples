using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectedServiceSample
{
    internal class Grid : ConnectedServiceGrid
    {
        private List<ConnectedServiceInstance> instances;

        public Grid()
        {
            this.Description = "A sample Connected Service";
            this.CreateServiceInstanceText = "Create";
            this.CanConfigureServiceInstance = true;
            this.ConfigureServiceInstanceText = "Configure";
        }

        public override IEnumerable<Tuple<string, string>> ColumnMetadata
        {
            get
            {
                yield return Tuple.Create("Column1", "Column1 Display");
            }
        }

        public override IEnumerable<Tuple<string, string>> DetailMetadata
        {
            get
            {
                yield return Tuple.Create("Detail1", "Detail1 Display");
                yield return Tuple.Create("Detail2", "Detail2 Display");
            }
        }

        public override Task<ConnectedServiceAuthenticator> CreateAuthenticatorAsync()
        {
            ConnectedServiceAuthenticator authenticator = new Authenticator();
            authenticator.AuthenticationChanged += (sender, e) =>
            {
                this.CanCreateServiceInstance = authenticator.IsAuthenticated;
            };

            return Task.FromResult(authenticator);
        }

        public override Task<IEnumerable<ConnectedServiceInstance>> EnumerateServiceInstancesAsync(CancellationToken ct)
        {
            // retrieve the service instances
            this.instances = new List<ConnectedServiceInstance>()
            {
                this.CreateInstance("#1", "1st column1", "1st detail1", "1st detail2"),
                this.CreateInstance("#2", "2nd column1", "2nd detail1", "2nd detail2"),
            };

            return Task.FromResult<IEnumerable<ConnectedServiceInstance>>(this.instances);
        }

        public override Task<bool> ConfigureServiceInstanceAsync(ConnectedServiceInstance instance, CancellationToken ct)
        {
            instance.Metadata["Column1"] = "Configured";

            return Task.FromResult(true);
        }

        public override Task<ConnectedServiceInstance> CreateServiceInstanceAsync(CancellationToken ct)
        {
            int instanceNumber = this.instances.Count + 1;
            ConnectedServiceInstance newInstance = this.CreateInstance(
                "#" + instanceNumber,
                instanceNumber + " column1",
                instanceNumber + " detail1",
                instanceNumber + " detail2");
            this.instances.Add(newInstance);

            return Task.FromResult(newInstance);
        }

        private ConnectedServiceInstance CreateInstance(string name, string column1, string detail1, string detail2)
        {
            ConnectedServiceInstance instance = new ConnectedServiceInstance();
            instance.Name = name;
            instance.InstanceId = name;
            instance.Metadata.Add("Column1", column1);
            instance.Metadata.Add("Detail1", detail1);
            instance.Metadata.Add("Detail2", detail2);
            return instance;
        }
    }

}
