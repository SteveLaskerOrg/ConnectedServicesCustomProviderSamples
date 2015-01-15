using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Windows.Input;

namespace ConnectedServiceSinglePageSample
{
    /// <summary>
    /// A simple authenticator that shows a "Sign In/Out" hyperlink
    /// </summary>
    internal class Authenticator : ConnectedServiceAuthenticator
    {
        private string linkText;
        private string userName;
        private ICommand changeAuthentication;

        public Authenticator()
        {
            this.LinkText = "Sign In";
            this.changeAuthentication = new ChangeAuthenticationCommand(this);

            this.View = new AuthenticatorView();
            this.View.DataContext = this;
        }

        /// <summary>
        /// Gets or sets the text of the hyperlink.
        /// </summary>
        public string LinkText
        {
            get { return this.linkText; }
            set
            {
                this.linkText = value;
                this.OnNotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the currently logged in user's name.
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
            set
            {
                this.userName = value;
                this.OnNotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The command that will be invoked when the user clicks the hyperlink.
        /// </summary>
        public ICommand ChangeAuthentication
        {
            get { return this.changeAuthentication; }
        }

        /// <summary>
        /// Fake out the signing in and out process by just toggling whether the
        /// user is signed in or out.
        /// </summary>
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
