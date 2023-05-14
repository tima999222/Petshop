using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для AddNewCharacteristicPage.xaml
    /// </summary>
    public partial class AddNewCharacteristicPage : Page
    {
        private Characteristics _currentCharacteristic = new Characteristics();
        public AddNewCharacteristicPage(Characteristics selectedCharacteristic)
        {
            InitializeComponent();

            if (selectedCharacteristic != null)
            {
                _currentCharacteristic = selectedCharacteristic;
            }

            DataContext = _currentCharacteristic;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrWhiteSpace(_currentCharacteristic.Characteristic))
            {
                errors.AppendLine("Введите значение");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentCharacteristic.CharacteristicId == 0)
            {
                PetshopEntities.GetContext().Characteristics.Add(_currentCharacteristic);
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
        }
    }
}
