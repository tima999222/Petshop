using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            updateProducts();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage((sender as Button).DataContext as Products));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var ProductsToRemove = DGridProducts.SelectedItems.Cast<Products>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PetshopEntities.GetContext().Products.RemoveRange(ProductsToRemove);
                    PetshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridProducts.ItemsSource = PetshopEntities.GetContext().Products.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void updateProducts()
        {
            var currentTours = PetshopEntities.GetContext().Products.ToList();

            currentTours = currentTours.Where(p => p.product.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            DGridProducts.ItemsSource = currentTours.ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateProducts();
        }

        private void CharacteristicsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsCharacteristicsPage((sender as Button).DataContext as Products));
        }
    }
}
