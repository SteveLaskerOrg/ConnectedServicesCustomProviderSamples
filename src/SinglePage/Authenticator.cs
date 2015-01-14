using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Windows.Input;

namespace ConnectedServiceSinglePageSample
{
    internal class Authenticator : ConnectedServiceAuthenticator
    {
        private string linkText;
        private string userName;
        private ICommand changeAuthentication;

        public Authenticator()
        {
            this.LinkText = "Sign in";
            this.changeAuthentication = new ChangeAuthenticationCommand(this);

            this.View = new AuthenticatorView();
            this.View.DataContext = this;
        }

        public string LinkText
        {
            get { return this.linkText; }
            set
            {
                this.linkText = value;
                this.OnNotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get { return this.userName; }
            set
            {
                this.userName = value;
                this.OnNotifyPropertyChanged();
            }
        }

        public ICommand ChangeAuthentication
        {
            get { return this.changeAuthentication; }
        }

        private void ExecuteChangeAuthentication()
        {
            this.IsAuthenticated = !this.IsAuthenticated;

            if (!this.IsAuthenticated)
            {
                this.LinkText = "Sign In";
                this.UserName = null;
            }
            else
            {
                this.LinkText = "Sign out";
                this.UserName = "someone@live.com";
            }

            this.OnAuthenticationChanged(new AuthenticationChangedEventArgs());
        }

        private class ChangeAuthenticationCommand : ICommand
        {
            private Authenticator authenticator;

            public ChangeAuthenticationCommand(Authenticator authenticator)
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
                this.authenticator.ExecuteChangeAuthentication();
            }
        }
    }
}
