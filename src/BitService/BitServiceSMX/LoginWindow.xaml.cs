/********************************************************
*                                                       *
*   Copyright (C) Microsoft. All rights reserved.       *
*                                                       *
********************************************************/

using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitServiceSMX
{
    public partial class LoginWindow : DialogWindow
    {
        public LoginWindow()
            : base()
        {
            InitializeComponent();
        }

        public string UserName;
        public string Password;

        private void login_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserName = usernameText.Text;
            Password = passwordText.Password;
            Close();
        }
    }
}
