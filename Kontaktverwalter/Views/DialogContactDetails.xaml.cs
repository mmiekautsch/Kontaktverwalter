using Kontaktverwalter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kontaktverwalter.Views
{
    /// <summary>
    /// Interaction logic for DialogContactDetails.xaml
    /// </summary>
    public partial class DialogContactDetails : Window
    {
        private readonly ViewModelDialogContactDetails _viewModel;

        public DialogContactDetails(long contactId)
        {
            InitializeComponent();
            _viewModel = new ViewModelDialogContactDetails();
            DataContext = _viewModel;
            _ = _viewModel.LoadContactDetailsAsync(contactId);
            dataGrid_Addresses.ItemsSource = _viewModel.Addresses;
            dataGrid_PhoneNumbers.ItemsSource = _viewModel.PhoneContacts;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
