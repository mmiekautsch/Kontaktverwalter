using Kontaktverwalter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktverwalter.Models
{
    /// <summary>
    /// Class representing the currently selected values in the contact manager
    /// </summary>
    class SelectedValuesContactManager : ObservableObject, IDataErrorInfo
    {
        private string? _txtNameSearchContent;

        public string? txtNameSearchContent
        {
            get => _txtNameSearchContent;
            set
            {
                if (txtNameSearchContent != value)
                {
                    _txtNameSearchContent = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                return ValidateNameSearchContent();
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(txtNameSearchContent): return ValidateNameSearchContent();
                    default:
                        break;
                }
                return null;
            }
        }

        private string? ValidateNameSearchContent()
        {
            if (txtNameSearchContent != null && txtNameSearchContent.Any(char.IsDigit))
            {
                return "Search content cannot contain digits.";
            }
            return null;
        }
    }

    /// <summary>
    /// Represents a contact information item with first and last name.
    /// </summary>
    class ContactInfoItem : ObservableObject, IDataErrorInfo
    {
        private string? _firstName;
        private string? _lastName;

        public long Id { get; set; } // just for identification purposes, not used in the UI

        public string? FirstName
        {
            get => _firstName;
            set
            {
                if (FirstName != value)
                {
                    _firstName = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        public string? LastName
        {
            get => _lastName;
            set
            {
                if (LastName != value)
                {
                    _lastName = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                return ValidateFirstName()
                    ?? ValidateLastName();
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName): return ValidateFirstName();
                    case nameof(LastName): return ValidateLastName();
                    default:
                        break;
                }

                return null;
            }
        }

        private string? ValidateFirstName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return "First name cannot be empty.";
            }
            if (FirstName.Any(char.IsDigit))
            {
                return "First name cannot contain digits.";
            }
            return null;
        }

        private string? ValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                return "Last name cannot be empty.";
            }
            if (LastName.Any(char.IsDigit))
            {
                return "Last name cannot contain digits.";
            }
            return null;
        }
    }
}
