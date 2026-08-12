using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kontaktverwalter.Utils
{
    /// <summary>
    /// Abstract base class for all objects that need to implement INotifyPropertyChanged, to avoid code duplication.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            if (propertyName != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                throw new ArgumentNullException(nameof(propertyName), "Property name cannot be null.");
            }
        }
    }
}
