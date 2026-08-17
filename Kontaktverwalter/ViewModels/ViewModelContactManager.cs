using Kontaktverwalter.Models;
using Kontaktverwalter.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kontaktverwalter.ViewModels
{
    class ViewModelContactManager
    {
        public SelectedValuesContactManager CurrentValues { get; }
        public ObservableCollection<ContactInfoItem> ContactInfoItems { get; }

        public ICommand SearchCommand { get; }

        public ViewModelContactManager()
        {
            ContactInfoItems = [];
            CurrentValues = new();

            SearchCommand = new AsyncRelayCommand(
                execute: SearchContactsAsync,
                canExecute: () => true
            );
        }

        private async Task SearchContactsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
