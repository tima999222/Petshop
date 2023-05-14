using System.Linq;
using System.Windows.Controls;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    { 
        public ProductList()
        {
            InitializeComponent();
            updateProducts();
        }
        /// <summary>
        /// Обновление списка продуктов при каждом изменении строки поиска
        /// </summary>
        private void updateProducts()
        {
            var currentTours = PetshopEntities.GetContext().Products.ToList();

            currentTours = currentTours.Where(p => p.product.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewProducts.ItemsSource = currentTours.ToList();
        }

        /// <summary>
        /// Событие изменения текста в TextBox для поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateProducts();
        }

        private void MoreInfo_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductInfoPage((sender as Button).DataContext as Products));
        }

        private void ToCart_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }
    }
}
