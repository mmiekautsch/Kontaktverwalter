using Kontaktverwalter.Models;
using Kontaktverwalter.Shared;
using Kontaktverwalter.Shared.DTO;
using Kontaktverwalter.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Kontaktverwalter.ViewModels
{
    public class ViewModelDialogContactDetails
    {
        private readonly ContactApiClient _apiClient;
        private ContactDetailDto? _contactDetail;

        public SelectedValuesContactDetails CurrentValues { get; }
        public ObservableCollection<AddressDto> Addresses { get; private set; }
        public ObservableCollection<PhoneContactDto> PhoneContacts { get; private set; }
        public ICommand SaveCommand { get; }

        public ViewModelDialogContactDetails()
        {
            _apiClient = new();
            CurrentValues = new();
            Addresses = [];
            PhoneContacts = [];

            SaveCommand = new AsyncRelayCommand(
                execute: UpdateContactNameAsync,
                canExecute: CanSave
            );
        }

        public async Task LoadContactDetailsAsync(long id)
        {
            try
            {
                _contactDetail = await _apiClient.GetContactDetailsAsync(id);
                if (_contactDetail != null)
                {
                    CurrentValues.FirstName = _contactDetail.FirstName;
                    CurrentValues.LastName = _contactDetail.LastName;
                    CurrentValues.DateOfBirth = _contactDetail.DateOfBirth.ToString("dd.MM.yyyy");

                    Addresses.Clear();
                    foreach (var address in _contactDetail.Addresses)
                    {
                        Addresses.Add(address);
                    }

                    PhoneContacts.Clear();
                    foreach (var phoneContact in _contactDetail.PhoneContacts)
                    {
                        PhoneContacts.Add(phoneContact);
                    }
                }
                else
                {
                    MessageBox.Show("Kontaktdetails konnten nicht geladen werden.", "Fehler");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Laden der Kontaktdetails: {ex.Message}", "Fehler");
            }
        }

        private bool CanSave()
        {
            return ((IDataErrorInfo)CurrentValues).Error == null
                && _contactDetail != null;
        }

        private async Task UpdateContactNameAsync()
        {
            try
            {
                var updateDto = new UpdateContactDto
                {
                    Id = _contactDetail!.IdPerson,
                    FirstName = CurrentValues.FirstName,
                    LastName = CurrentValues.LastName
                };

                await _apiClient.UpdateContactAsync(updateDto);
                MessageBox.Show("Kontakt erfolgreich aktualisiert.", "Erfolg");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Aktualisieren des Kontakts: {ex.Message}", "Fehler");
            }
        }
    }
}
