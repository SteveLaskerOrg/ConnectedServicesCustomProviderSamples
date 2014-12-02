using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectedServiceSample
{
    internal class ConnectedServiceProviderGrid :
        IConnectedServiceProviderGridUI, IConnectedServiceHyperlinkAuthenticator, IConnectedServiceInstanceConfigurer, IConnectedServiceInstanceCreator,
        IDisposable, INotifyPropertyChanged
    {
        private ConnectedServiceProvider provider;
        private bool disposed;
        private List<IConnectedServiceInstance> instances;

        public event PropertyChangedEventHandler PropertyChanged;

        public ConnectedServiceProviderGrid(ConnectedServiceProvider provider)
        {
            this.provider = provider;

            this.ChangeAuthenticationText = "Sign in";
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                // uncache the CurrentProviderGrid, so a new one is created the next time the dialog is opened
                this.provider.CurrentProviderGrid = null;
                this.disposed = true;
            }
        }

        public string EnumeratingServiceInstancesText { get { return null; } }
        public string GridHeaderText { get { return null; } }
        public string NoServiceInstancesText { get { return null; } }
        public string ServiceInstanceNameLabelText { get { return null; } }
        public string ShortDescription { get { return "A sample Connected Service"; } }

        public IEnumerable<Tuple<string, string>> ColumnMetadata
        {
            get
            {
                yield return Tuple.Create("Column1", "Column1 Display");
            }
        }

        public IEnumerable<Tuple<string, string>> DetailMetadata
        {
            get
            {
                yield return Tuple.Create("Detail1", "Detail1 Display");
                yield return Tuple.Create("Detail2", "Detail2 Display");
            }
        }

        public Task<IEnumerable<IConnectedServiceInstance>> EnumerateServiceInstancesAsync(
            IDictionary<string, string> configuration, CancellationToken ct)
        {
            if (this.instances == null)
            {
                // retrieve the service instances
                this.instances = new List<IConnectedServiceInstance>()
                {
                    new ConnectedServiceInstance("#1", "1st column1", "1st detail1", "1st detail2"),
                    new ConnectedServiceInstance("#2", "2nd column1", "2nd detail1", "2nd detail2"),
                };
            }

            return Task.FromResult<IEnumerable<IConnectedServiceInstance>>(this.instances);
        }

        #region IConnectedServiceAuthenticator
        private bool isAuthenticated;
        public bool IsAuthenticated
        {
            get { return this.isAuthenticated; }
            private set { this.Set(ref this.isAuthenticated, value); }
        }

        public string NeedToAuthenticateText
        {
            get { return null; }
        }

        private string authenticatedText;
        public string AuthenticatedText
        {
            get { return this.authenticatedText; }
            private set { this.Set(ref this.authenticatedText, value); }
        }

        private string changeAuthenticationText;
        public string ChangeAuthenticationText
        {
            get { return this.changeAuthenticationText; }
            private set { this.Set(ref this.changeAuthenticationText, value); }
        }

        public Task<bool> ChangeAuthentication(IDictionary<string, string> configuration, CancellationToken ct)
        {
            this.IsAuthenticated = !this.IsAuthenticated;
            this.CanCreateServiceInstance = this.IsAuthenticated;

            if (this.IsAuthenticated)
            {
                this.AuthenticatedText = "someone@live.com";
                this.ChangeAuthenticationText = "Sign out";
            }
            else
            {
                this.AuthenticatedText = null;
                this.ChangeAuthenticationText = "Sign in";
            }

            return Task.FromResult(true);
        }
        #endregion

        #region IConnectedServiceInstanceConfigurer
        public bool CanConfigureServiceInstance
        {
            get
            {
                return true;
            }
        }

        public string ConfigureServiceInstanceText
        {
            get
            {
                return "Configure";
            }
        }

        public Task<bool> ConfigureServiceInstance(IDictionary<string, string> configuration, IConnectedServiceInstance instance, CancellationToken ct)
        {
            ((ConnectedServiceInstance)instance).SetColumn1("Configured");

            return Task.FromResult(true);
        }
        #endregion

        #region IConnectedServiceInstanceCreator
        private bool canCreateServiceInstance;
        public bool CanCreateServiceInstance
        {
            get { return this.canCreateServiceInstance; }
            private set { this.Set(ref this.canCreateServiceInstance, value); }
        }

        public string CreateServiceInstanceText
        {
            get
            {
                return "Create";
            }
        }

        public Task<IConnectedServiceInstance> CreateServiceInstance(IDictionary<string, string> configuration, CancellationToken ct)
        {
            int instanceNumber = this.instances.Count + 1;
            IConnectedServiceInstance newInstance = new ConnectedServiceInstance(
                "#" + instanceNumber,
                instanceNumber + " column1",
                instanceNumber + " detail1",
                instanceNumber + " detail2");
            this.instances.Add(newInstance);

            return Task.FromResult(newInstance);
        }
        #endregion

        private bool Set<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                this.NotifyPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
