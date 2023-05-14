using System.Windows;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для DataEditWindow.xaml
    /// </summary>
    public partial class DataEditWindow : Window
    {
        public DataEditWindow()
        {
            InitializeComponent();
        }

        private void FirmsButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new FirmsPage());
        }

        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new CategoriesPage());
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ProductsPage());
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new OrdersPage());
        }

        private void CharacteristicsButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new CharacteristicsPage());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Session.SessionIdClient = 0;
        }
    }
}
