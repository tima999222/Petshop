using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для ProductsInOrderPage.xaml
    /// </summary>
    public partial class ProductsInOrderPage : Page
    {
        public ProductsInOrderPage(Orders selectedOrder)
        {
            InitializeComponent();

            DataContext = selectedOrder;

            var CartList = (
                                            from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                            join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                            join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                            join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                            where o.id_order == selectedOrder.id_order
                                            select po
                                           ).ToList();


            DGridProductsInOrder.ItemsSource = CartList;

            decimal total = 0;
            foreach (var p in CartList)
            {
                total += (decimal)(p.Products.price * p.count_of_products);
            }
            totalPriceLabel.Content = $"Итого: {total} руб.";

        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
