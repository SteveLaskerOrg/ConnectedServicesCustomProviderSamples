using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
namespace Microsoft.ConnectedServices.Samples.ViewModels
{
    internal class AuthenticatorViewModel : ConnectedServiceAuthenticator
    {

        public AuthenticatorViewModel()
        {
            this.LoginLinkText = "Sign in";

            this.View = new Views.AuthenticatorView();
            this.View.DataContext = this;
            this.IsAuthenticated = true;
        }

        private string loginLinkText;
        public string LoginLinkText
        {
            get { return this.loginLinkText; }
            set
            {
                this.loginLinkText = value;
                this.OnNotifyPropertyChanged();
            }
        }

        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set
            {
                this.userName = value;
                this.OnNotifyPropertyChanged();
            }
        }

        private void ChangeAuthentication()
        {
            this.IsAuthenticated = !this.IsAuthenticated;

            if (!this.IsAuthenticated)
            {
                this.LoginLinkText = "Sign In";
                this.UserName = null;
            }
            else
            {
                this.LoginLinkText = "Sign out";
                this.UserName = "someone@live.com";
            }

            this.OnAuthenticationChanged(new AuthenticationChangedEventArgs());
        }

        private class AuthenticateChangedCommand : ICommand
        {
            private AuthenticatorViewModel authenticator;

            public AuthenticateChangedCommand(AuthenticatorViewModel authenticator)
            {
                this.authenticator = authenticator;
            }

            public event EventHandler CanExecuteChanged { add { } remove { } }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                this.authenticator.ChangeAuthentication();
            }
        }
    }
}