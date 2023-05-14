using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();

            var CartList = (
                                            from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                            join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                            join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                            join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                            where c.id_client == Session.SessionIdClient && o.date_of_order == null
                                            select po
                                       ).ToList();

            DGridCart.ItemsSource = CartList;
            decimal total = 0;
            foreach (var p in CartList)
            {
                total += (decimal)(p.Products.price * p.count_of_products);
            }
            totalPriceLabel.Content = $"Итого: {total} руб.";
        }

        private void GoBack_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Оформление заказа по нажатию на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateOrder_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var cart = (
                            from po in PetshopEntities.GetContext().Products_in_order.ToList()
                            join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                            join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                            join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                            where c.id_client == Session.SessionIdClient && o.date_of_order == null
                            select po
                        ).ToList();
            if (cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста");
                return;
            }

            SelectAddressWindow selectAddressWindow = new SelectAddressWindow();

            if (selectAddressWindow.ShowDialog() == true)
            {
                var currentOrder = PetshopEntities.GetContext().Orders.FirstOrDefault(o=>o.id_client == Session.SessionIdClient && o.date_of_order == null);
                currentOrder.date_of_order = DateTime.UtcNow;
                currentOrder.delivery_address = selectAddressWindow.Address;
                PetshopEntities.GetContext().SaveChanges();
                MessageBox.Show("Заказ оформлен");
                currentOrder.GenerateCheck();

                DGridCart.ItemsSource = (
                                            from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                            join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                            join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                            join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                            where c.id_client == Session.SessionIdClient && o.date_of_order == null
                                            select po
                                        ).ToList();
                totalPriceLabel.Content = "Итого: 0 руб.";

            }
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = DGridCart.SelectedItem;   
            int count = Convert.ToInt32(selectedProduct.GetType().GetProperty("count_of_products").GetValue(selectedProduct));
            if (count > 1)
            {
                count--;
                selectedProduct.GetType().GetProperty("count_of_products").SetValue(selectedProduct, count);

                var CartList = (
                                                from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                                join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                                join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                                join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                                where c.id_client == Session.SessionIdClient && o.date_of_order == null
                                                select po
                                            ).ToList();
                DGridCart.ItemsSource = CartList;
                try
                {
                    PetshopEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                decimal total = 0;
                foreach (var p in CartList)
                {
                    total += (decimal)(p.Products.price * p.count_of_products);
                }
                totalPriceLabel.Content = $"Итого: {total} руб.";
            }
            else
            {
                var productsToRemove = DGridCart.SelectedItems.Cast<Products_in_order>().ToList();

                if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        PetshopEntities.GetContext().Products_in_order.RemoveRange(productsToRemove);
                        PetshopEntities.GetContext().SaveChanges();
                        MessageBox.Show("Товар удален из корзины");

                        var CartList = (
                                                from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                                join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                                join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                                join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                                where c.id_client == Session.SessionIdClient && o.date_of_order == null
                                                select po
                                            ).ToList();
                        DGridCart.ItemsSource = CartList;
                        decimal total = 0;
                        foreach (var p in CartList)
                        {
                            total += (decimal)(p.Products.price * p.count_of_products);
                        }
                        totalPriceLabel.Content = $"Итого: {total} руб.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    selectedProduct.GetType().GetProperty("count_of_products").SetValue(selectedProduct, count);
                }
            }
        }

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = DGridCart.SelectedItem;
            int count = Convert.ToInt32(selectedProduct.GetType().GetProperty("count_of_products").GetValue(selectedProduct));
            count++;
            selectedProduct.GetType().GetProperty("count_of_products").SetValue(selectedProduct, count);

            try
            {
                PetshopEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var CartList = (
                                            from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                            join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                            join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                            join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                            where c.id_client == Session.SessionIdClient && o.date_of_order == null
                                            select po
                                        ).ToList();
            DGridCart.ItemsSource = CartList;
            decimal total = 0;
            foreach (var p in CartList)
            {
                total += (decimal)(p.Products.price * p.count_of_products);
            }
            totalPriceLabel.Content = $"Итого: {total} руб.";
        }

        private void MyOrders_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientOrdersPage());
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var productsToRemove = DGridCart.SelectedItems.Cast<Products_in_order>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PetshopEntities.GetContext().Products_in_order.RemoveRange(productsToRemove);
                    PetshopEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    DGridCart.ItemsSource = (
                                            from po in PetshopEntities.GetContext().Products_in_order.ToList()
                                            join o in PetshopEntities.GetContext().Orders.ToList() on po.id_order equals o.id_order
                                            join c in PetshopEntities.GetContext().Clients.ToList() on o.id_client equals c.id_client
                                            join p in PetshopEntities.GetContext().Products.ToList() on po.id_product equals p.id_product
                                            where c.id_client == Session.SessionIdClient && o.date_of_order == null
                                            select po
                                        ).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
