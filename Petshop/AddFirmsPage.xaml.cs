using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для AddFirmsPage.xaml
    /// </summary>
    public partial class AddFirmsPage : Page
    {
        private Firms _currentFirm = new Firms();

        public AddFirmsPage(Firms selectedFirm)
        {
            InitializeComponent();

            if (selectedFirm != null)
            {
                _currentFirm = selectedFirm;
            }

            DataContext = _currentFirm;
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            _currentFirm.firm = FirmTextBox.Text;

            if (string.IsNullOrWhiteSpace(_currentFirm.firm)) 
            {
                MessageBox.Show("Введите фирму");
                return;
            }
            if (_currentFirm.id_firm == 0)
            {
                PetshopEntities.GetContext().Firms.Add(_currentFirm);
            }
            try
            {
                PetshopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FirmTextBox.Clear();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
