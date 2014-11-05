using Company.ConnectedServiceDemo;
using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Company.ConnectedServiceDemo {
    [Export(typeof(IConnectedServiceProvider))]
    [ExportMetadata("ProviderId", "Microsoft.VisualStudio.ConnectedServices.Sample.WizardProvider")]
    [ExportMetadata("Version", "1.0")]
    internal class WizardDemoProvider : NotifyPropertyChangeBase, IConnectedServiceProvider, IConnectedServiceProviderWizardUI, IConnectedServiceLinkProvider {
        private ObservableCollection<IConnectedServiceWizardPage> pages;
        private WizardDemoInstance instance;

        public event EventHandler<EnableNavigationEventArgs> EnableNavigation;

        public WizardDemoProvider() {
            this.pages = new ObservableCollection<IConnectedServiceWizardPage>();
            this.pages.Add(new ViewModels.Page1ViewModel(this));
            this.pages.Add(new ViewModels.Page2ViewModel(this));
            this.pages.Add(new ViewModels.Page3ViewModel(this));
            this.pages.Add(new ViewModels.Page4ViewModel(this));
        }

        public string Name {
            get { return "Sample Wizard Provider"; }
        }

        public string Category {
            get { return "MySamples"; }
        }

        public string Description {
            get { return "Sample Provider with Wizard functionality."; }
        }

        public ImageSource Icon {
            get {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri("pack://application:,,/" + Assembly.GetAssembly(this.GetType()).ToString() + ";component/Wizard.jpg");
                image.EndInit();
                return image;
            }
        }

        public string CreatedBy {
            get { return "Steve Lasker"; }
        }

        public Uri MoreInfoUri {
            get { return new Uri("https://github.com/SteveLasker/ConnectedServiceProviderSample"); }
        }

        public object CreateService(Type serviceType) {
            if (serviceType == typeof(IConnectedServiceProviderUI)) {
                return this;
            } else if (serviceType == typeof(IConnectedServiceLinkProvider)) {
                return this;
            }

            return null;
        }

        public ObservableCollection<IConnectedServiceWizardPage> Pages {
            get { return this.pages; }
        }

        public Task<IConnectedServiceInstance> GetFinishedServiceInstance() {
            return Task.FromResult<IConnectedServiceInstance>(this.Instance);
        }

        // used in the unit test
        public WizardDemoInstance Instance {
            get {
                if (this.instance == null) {
                    this.instance = new WizardDemoInstance("wizard", "wizard",
                        "Microsoft.VisualStudio.ConnectedServices.Sample.WizardProvider");
                }
                return this.instance;
            }
        }

        public IEnumerable<Tuple<string, Uri>> GetSupportedTechnologyLinks() {
            return null;
        }

        internal void OnEnableNavigation(NavigationEnabledState state) {
            if (this.EnableNavigation != null) {
                this.EnableNavigation(this, new EnableNavigationEventArgs() { State = state });
            }
        }
    }
}