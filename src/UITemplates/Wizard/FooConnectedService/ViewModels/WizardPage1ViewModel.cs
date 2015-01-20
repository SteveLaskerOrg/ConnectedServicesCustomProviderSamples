using Microsoft.VisualStudio.ConnectedServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ConnectedServices.Samples.ViewModels
{
    internal class WizardPage1ViewModel : ConnectedServiceWizardPage
    {
        public WizardPage1ViewModel() : base()
        {
            this.Title = "Page 1";
            this.Description = "Page 1 Description";
            this.Legend = "Page 1 Legend";
            this.View = new Views.WizardPage1Viiew();
            this.View.DataContext = this;
        }
    }
}
