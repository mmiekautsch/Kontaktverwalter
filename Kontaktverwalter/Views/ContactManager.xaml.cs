using Kontaktverwalter.Models;
using Kontaktverwalter.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kontaktverwalter.Views
{
    /// <summary>
    /// Interaction logic for ContactManager.xaml
    /// </summary>
    public partial class ContactManager : Window
    {
        private readonly ViewModelContactManager _viewModel;
        public ContactManager()
        {
            InitializeComponent();
            _viewModel = new ViewModelContactManager();
            DataContext = _viewModel;

            dataGrid_ContactView.ItemsSource = _viewModel.ContactInfoItems;
        }

        private void dataGrid_ContactView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement(dataGrid_ContactView, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row?.Item is ContactInfoItem selectedContact)
            {
                var detailsDialog = new DialogContactDetails(selectedContact.Id)
                {
                    Owner = this
                };
                detailsDialog.ShowDialog();
            }
        }
    }
}