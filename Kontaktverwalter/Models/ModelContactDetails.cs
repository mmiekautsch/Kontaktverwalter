using Kontaktverwalter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktverwalter.Models
{
    /// <summary>
    /// Class representing the currently displayed contact details values
    /// </summary>
    public class SelectedValuesContactDetails : ObservableObject, IDataErrorInfo
    {
        private string? _firstName;
        private string? _lastName;
        private string? _dateOfBirth;

        public string? FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
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
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        public string? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
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
            if (FirstName == null) return null;
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
            if (LastName == null) return null;
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
