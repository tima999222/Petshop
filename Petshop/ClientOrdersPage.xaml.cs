using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Diagnostics;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для ClientOrdersPage.xaml
    /// </summary>
    public partial class ClientOrdersPage : Page
    {
        public ClientOrdersPage()
        {
            InitializeComponent();
            DGridOrders.ItemsSource = PetshopEntities.GetContext().Orders.Where(o => o.date_of_order != null && o.id_client == Session.SessionIdClient).ToList();
        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsInOrderPage((sender as Button).DataContext as Orders));
        }

        private void ShowCheckButton_Click(object sender, RoutedEventArgs e)
        {
            Orders selectedOrder = DGridOrders.SelectedItem as Orders;
            string path = @"C:\Users\Тимофей\Desktop\Курсовая\source\checks\Order" + selectedOrder.id_order.ToString() + ".docx";
            if (!File.Exists(path))
            { 
                selectedOrder.GenerateCheck();
            }
            Process.Start(path);
        }
    }
}
