using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
namespace Microsoft.ConnectedServices.Samples.ViewModels {
    internal class AuthenticatorViewModel : ConnectedServiceAuthenticator {
        public AuthenticatorViewModel() {
            this.View = new Views.AuthenticatorView();
            this.View.DataContext = this;
        }
    }
}