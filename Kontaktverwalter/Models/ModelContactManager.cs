using Kontaktverwalter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontaktverwalter.Models
{
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

        public string? ValidateNameSearchContent()
        {
            if (string.IsNullOrWhiteSpace(txtNameSearchContent))
            {
                return "Search content is required.";
            }
            return null;
        }
    }

    class ContactInfoItem : ObservableObject, IDataErrorInfo
    {
        private string? _firstName;
        private string? _lastName;

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

        public string? ValidateFirstName()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return "First name is required.";
            }
            return null;
        }

        public string? ValidateLastName()
        {
            if (string.IsNullOrWhiteSpace(LastName))
            {
                return "Last name is required.";
            }
            return null;
        }
    }
}
