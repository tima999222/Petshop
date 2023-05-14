using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для Firms1.xaml
    /// </summary>
    public partial class FirmsPage : Page
    {
        public FirmsPage()
        {
            InitializeComponent();
            DGridFirms.ItemsSource = PetshopEntities.GetContext().Firms.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddFirmsPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddFirmsPage((sender as Button).DataContext as Firms));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var FirmsToRemove = DGridFirms.SelectedItems.Cast<Firms>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PetshopEntities.GetContext().Firms.RemoveRange(FirmsToRemove);
                    PetshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridFirms.ItemsSource = PetshopEntities.GetContext().Firms.ToList();
                }
                catch (Exception ex)
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
                DGridFirms.ItemsSource = PetshopEntities.GetContext().Firms.ToList();
            }
        }
    }
}
