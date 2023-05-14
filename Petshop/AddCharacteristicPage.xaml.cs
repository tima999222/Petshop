using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для AddCharacteristicPage.xaml
    /// </summary>
    public partial class AddCharacteristicPage : Page
    {
        private ProductCharacteristics _currentProductCharacteristic = new ProductCharacteristics();

        public AddCharacteristicPage(Products selectedProduct, ProductCharacteristics selectedProductCharacteristics)
        {
            InitializeComponent();
            CharacteristicsComboBox.ItemsSource = PetshopEntities.GetContext().Characteristics.ToList();

            _currentProductCharacteristic.ProductId = selectedProduct.id_product;


            if (selectedProductCharacteristics != null)
            {
                _currentProductCharacteristic = selectedProductCharacteristics;
            }

            DataContext = _currentProductCharacteristic;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            var list = (
                        from pcv in PetshopEntities.GetContext().ProductCharacteristics.ToList()
                        join c in PetshopEntities.GetContext().Characteristics.ToList() on pcv.CharacteristicId equals c.CharacteristicId
                        where pcv.ProductId == _currentProductCharacteristic.ProductId
                        select pcv
                       ).ToList();

            StringBuilder errors = new StringBuilder();

            if (_currentProductCharacteristic.Characteristics == null)
                errors.AppendLine("Выберите характеристику");
            if (string.IsNullOrWhiteSpace(_currentProductCharacteristic.СharacteristicValue))
                errors.AppendLine("Введите значение");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (list.FirstOrDefault(p => p.ProductId == _currentProductCharacteristic.ProductId && p.CharacteristicId == _currentProductCharacteristic.CharacteristicId) == null)
            {
                PetshopEntities.GetContext().ProductCharacteristics.Add(_currentProductCharacteristic);
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
