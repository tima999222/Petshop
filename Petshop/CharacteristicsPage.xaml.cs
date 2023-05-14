using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Linq;
using System;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для CharacteristicsPage.xaml
    /// </summary>
    public partial class CharacteristicsPage : Page
    {
        public CharacteristicsPage()
        {
            InitializeComponent();
            DGridCharacteristics.ItemsSource = PetshopEntities.GetContext().Characteristics.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewCharacteristicPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewCharacteristicPage((sender as Button).DataContext as Characteristics));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var CharacteristcsToRemove = DGridCharacteristics.SelectedItems.Cast<Characteristics>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PetshopEntities.GetContext().Characteristics.RemoveRange(CharacteristcsToRemove);
                    PetshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCharacteristics.ItemsSource = PetshopEntities.GetContext().Characteristics.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }       
    }
}
