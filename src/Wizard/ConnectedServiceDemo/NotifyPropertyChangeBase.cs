using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Company.ConnectedServiceDemo
{
    /// <summary>
    /// Provides basic functionality for any class to send
    /// INotifyPropertyChanged events.
    /// </summary>
    internal abstract class NotifyPropertyChangeBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies clients that a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A utility setter that automatically fires a PropertyChanged
        /// notification if the value has changed.  This is intended to
        /// make simple get/set properties easier to author:
        /// <code>
        /// private int someProp;
        /// public int SomeProp {
        ///     get { return this.someProp; }
        ///     set { this.Set(ref this.someProp, value); }
        /// }
        /// </code>
        /// </summary>
        /// <typeparam name="T">The type of the property and field.</typeparam>
        /// <param name="backingField">The property backing field.</param>
        /// <param name="value">The new value.</param>
        /// <param name="propertyName">The name of the property that's changing.</param>
        /// <returns>Returns true if the field has actually changed; false otherwise.</returns>
        protected bool Set<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(backingField, value))
            {
                backingField = value;
                this.NotifyPropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Notifies clients that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that changed.
        /// </param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler temp = this.PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
