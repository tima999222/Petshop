using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using System.Diagnostics;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            DGridOrders.ItemsSource = PetshopEntities.GetContext().Orders.Where(d => d.date_of_order != null).ToList();
        }

        private void SetDevileredButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var SelectedOrder = (Orders)DGridOrders.SelectedItem;
                if (SelectedOrder.date_of_issue != null)
                {
                    MessageBox.Show("Этот заказ уже выдан");
                    return;
                }

                SelectedOrder.date_of_issue = DateTime.Now;
                MessageBox.Show("Заказ выдан");
                PetshopEntities.GetContext().SaveChanges();
                DGridOrders.ItemsSource = PetshopEntities.GetContext().Orders.Where(d => d.date_of_order != null).ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
