using Microsoft.VisualStudio.ConnectedServices;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Company.ConnectedServiceDemo.ViewModels
{
    internal class WizardPage : ConnectedServiceWizardPage
    {
        internal bool CanFinish { get; set; }

        public override Task<NavigationEnabledState> OnPageEnteringAsync(WizardEnteringArgs args)
        {
            return Task.FromResult<NavigationEnabledState>(new NavigationEnabledState(null, null, this.CanFinish));
        }

        protected bool Set<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                this.OnNotifyPropertyChanged(propertyName);
                return true;
            }

            return false;
        }
    }
}
