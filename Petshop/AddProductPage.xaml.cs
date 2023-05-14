using Microsoft.Win32;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml_currentProduct
    /// </summary>
    public partial class AddProductPage : Page
    {
        private Products _currentProduct = new Products();

        public AddProductPage(Products selectedProduct)
        {
            InitializeComponent();
            
            FirmsComboBox.ItemsSource = PetshopEntities.GetContext().Firms.ToList();
            CategoryComboBox.ItemsSource = PetshopEntities.GetContext().Categories.ToList();

            if (selectedProduct != null)
            {
                _currentProduct = selectedProduct;
            }

            DataContext = _currentProduct;
        }

        private void AddProductPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ProductPicLabel.Content = openFileDialog.FileName;
                _currentProduct.picture = openFileDialog.FileName;
            }
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentProduct.product))
                errors.AppendLine("Введите название товара");
            if (_currentProduct.Firms == null)
                errors.AppendLine("Выберите фирму");
            if (_currentProduct.Categories == null)
                errors.AppendLine("Выберите категорию");
            if (_currentProduct.picture == null)
                _currentProduct.picture = null;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProduct.id_product == 0)
            {
                PetshopEntities.GetContext().Products.Add(_currentProduct);
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

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
