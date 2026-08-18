using Kontaktverwalter.Models;
using Kontaktverwalter.Shared;
using Kontaktverwalter.Shared.DTO;
using Kontaktverwalter.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kontaktverwalter.ViewModels
{
    class ViewModelContactManager
    {
        private readonly ContactApiClient _apiClient;
        private List<ContactDto> _allCurrentContacts; // all contacts retrieved from the last search

        public SelectedValuesContactManager CurrentValues { get; }
        public ObservableCollection<ContactInfoItem> ContactInfoItems { get; }

        public ICommand SearchCommand { get; }

        public ViewModelContactManager()
        {
            ContactInfoItems = [];
            CurrentValues = new();
            _apiClient = new();
            _allCurrentContacts = [];

            SearchCommand = new AsyncRelayCommand(
                execute: SearchContactsAsync,
                canExecute: () => true
            );
        }

        private async Task SearchContactsAsync()
        {
            ContactInfoItems.Clear();
            if (CurrentValues.txtNameSearchContent.IsNullOrEmpty())
            {
                _allCurrentContacts = await _apiClient.GetAllContactsAsync() ?? [];
            }
            else
            {
                _allCurrentContacts = await _apiClient.SearchContactsAsync(CurrentValues.txtNameSearchContent!) ?? [];
            }
            _allCurrentContacts = [.. _allCurrentContacts.OrderBy(c => c.LastName).ThenBy(c => c.FirstName)];

            foreach (ContactDto contact in _allCurrentContacts)
            {
                if (!ContactInfoItems.Any(item => item.Id == contact.IdPerson))
                {
                    ContactInfoItems.Add(new ContactInfoItem() { Id = contact.IdPerson, FirstName = contact.FirstName, LastName = contact.LastName });
                }
            }  
        }
    }
}
