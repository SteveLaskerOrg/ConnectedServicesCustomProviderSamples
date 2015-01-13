using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ConnectedServiceSinglePageSample
{
    internal class Authenticator : ConnectedServiceAuthenticator
    {
        private string linkText;
        private string userText;

        public Authenticator()
        {
            this.LinkText = "Sign in";
            Run run = new Run();
            run.SetBinding(Run.TextProperty, "LinkText");
            Hyperlink hyperlink = new Hyperlink(run);
            hyperlink.Command = new AuthenticateChangedCommand(this);
            TextBlock linkTextBlock = new TextBlock(hyperlink);

            TextBlock userTextBlock = new TextBlock();
            userTextBlock.SetBinding(TextBlock.TextProperty, "UserText");

            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(linkTextBlock);
            stackPanel.Children.Add(userTextBlock);

            this.View = stackPanel;
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

        public string UserText
        {
            get { return this.userText; }
            set
            {
                this.userText = value;
                this.OnNotifyPropertyChanged();
            }
        }

        private void ChangeAuthentication()
        {
            this.IsAuthenticated = !this.IsAuthenticated;

            if (!this.IsAuthenticated)
            {
                this.LinkText = "Sign In";
                this.UserText = null;
            }
            else
            {
                this.LinkText = "Sign out";
                this.UserText = "someone@live.com";
            }

            this.OnAuthenticationChanged(new AuthenticationChangedEventArgs());
        }

        private class AuthenticateChangedCommand : ICommand
        {
            private Authenticator authenticator;

            public AuthenticateChangedCommand(Authenticator authenticator)
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
