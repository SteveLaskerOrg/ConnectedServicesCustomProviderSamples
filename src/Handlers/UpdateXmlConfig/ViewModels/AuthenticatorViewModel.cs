﻿using Microsoft.VisualStudio.ConnectedServices;

namespace Microsoft.ConnectedServices.Samples.Handlers.UpdateXmlConfig.ViewModels
{
    internal class AuthenticatorViewModel : ConnectedServiceAuthenticator
    {
        public AuthenticatorViewModel()
        {
            this.View = new Views.AuthenticatorView();
            this.View.DataContext = this;
        }
    }
}