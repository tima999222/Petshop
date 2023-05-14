using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для AddCategoryPage.xaml
    /// </summary>
    public partial class AddCategoryPage : Page
    {
        private Categories _currentCategory = new Categories();

        public AddCategoryPage(Categories selectedCategory) 
        {
            InitializeComponent();

            if (selectedCategory != null)
            {
                _currentCategory = selectedCategory; 
            }

            DataContext = _currentCategory;
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentCategory.category))
            {
                MessageBox.Show("Введите категорию");
                return;
            }
            if (_currentCategory.id_category == 0)
            {
                PetshopEntities.GetContext().Categories.Add(_currentCategory);
            }
            try
            {
                PetshopEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CategoryTextBox.Clear();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
