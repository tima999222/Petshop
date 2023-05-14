using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        public CategoriesPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage((sender as Button).DataContext as Categories));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var categoriesToRemove = DGridCategories.SelectedItems.Cast<Categories>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PetshopEntities.GetContext().Categories.RemoveRange(categoriesToRemove);
                    PetshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCategories.ItemsSource = PetshopEntities.GetContext().Categories.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Page_isVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                PetshopEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridCategories.ItemsSource = PetshopEntities.GetContext().Categories.ToList();
            }
        }
    }
}
