using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для ProductsCharacteristicsPage.xaml
    /// </summary>
    public partial class ProductsCharacteristicsPage : Page
    {

        private Products _currentProduct = new Products();
        public ProductsCharacteristicsPage(Products selectedProduct)
        {
            InitializeComponent();
            _currentProduct = selectedProduct;
            DGridCharacteristics.ItemsSource = (
                                                from pcv in PetshopEntities.GetContext().ProductCharacteristics.ToList()
                                                join c in PetshopEntities.GetContext().Characteristics.ToList() on pcv.CharacteristicId equals c.CharacteristicId
                                                where pcv.ProductId == _currentProduct.id_product
                                                select pcv
                                               ).ToList();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // NavigationService.Navigate(new AddCharacteristicPage(_currentProduct, (sender as Button).DataContext as ProductCharacteristics));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCharacteristicPage(_currentProduct, null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var CharacteristicsToRemove = DGridCharacteristics.SelectedItems.Cast<ProductCharacteristics>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PetshopEntities.GetContext().ProductCharacteristics.RemoveRange(CharacteristicsToRemove);
                    PetshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCharacteristics.ItemsSource = (
                                                from pcv in PetshopEntities.GetContext().ProductCharacteristics.ToList()
                                                join c in PetshopEntities.GetContext().Characteristics.ToList() on pcv.CharacteristicId equals c.CharacteristicId
                                                where pcv.ProductId == _currentProduct.id_product
                                                select pcv
                                               ).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
