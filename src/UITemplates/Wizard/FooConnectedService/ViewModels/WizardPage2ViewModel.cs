﻿using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.ViewModels
{
    internal class WizardPage2ViewModel : ConnectedServiceWizardPage
    {
        public WizardPage2ViewModel() : base()
        {
            this.Title = "Page 2";
            this.Description = "Page 2 Description w/Erroz";
            this.Legend = "Page 2 Legend";
            this.View = new Views.WizardPage2View();
            this.View.DataContext = this;
            // Show the error glyph in the page navigation
            this.HasErrors = true;
        }
    }
}