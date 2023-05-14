using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для ProductInfoPage.xaml
    /// </summary>
    public partial class ProductInfoPage : Page
    {
        private Products _currrentProduct = new Products();
        public ProductInfoPage(Products selectedProduct)
        {
            InitializeComponent();
            _currrentProduct = selectedProduct;
            DataContext = _currrentProduct;
            DGridCharacteristics.ItemsSource = (
                                                from pcv in PetshopEntities.GetContext().ProductCharacteristics.ToList()
                                                join c in PetshopEntities.GetContext().Characteristics.ToList() on pcv.CharacteristicId equals c.CharacteristicId
                                                where pcv.ProductId == _currrentProduct.id_product
                                                select pcv
                                               ).ToList();
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(Count_TextBox.Text);
            if (count > 0)
            {
                count--;
                Count_TextBox.Text = count.ToString();
            }
        }

        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(Count_TextBox.Text);
            count++;
            Count_TextBox.Text = count.ToString();
        }
        /// <summary>
        /// Добавление товара в корзину по нажатию на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCart_Button_Click(object sender, RoutedEventArgs e)
        {
            if(int.Parse(Count_TextBox.Text) <= 0)
            {
                MessageBox.Show("Вы не добавили ни одного товара");
                return;
            }

            try
            {
                var orders = PetshopEntities.GetContext().Orders.ToList();
                var products_in_order = PetshopEntities.GetContext().Products_in_order.ToList();
                var clients = PetshopEntities.GetContext().Clients.ToList();
                var product = PetshopEntities.GetContext().Products.ToList();

                var isNewOrder = PetshopEntities.GetContext().Orders.FirstOrDefault(o => o.id_client == Session.SessionIdClient && o.date_of_order == null);
                
                var isNewProducts = from po in products_in_order
                                   join o in orders on po.id_order equals o.id_order
                                   join c in clients on o.id_client equals c.id_client
                                   join p in product on po.id_product equals p.id_product
                                   where c.id_client == Session.SessionIdClient && p.id_product == _currrentProduct.id_product
                                   select po;
                
                Orders order = null;
                Products_in_order products_In_Order = null;

                if (isNewOrder == null)
                {
                    order = new Orders
                    {
                        id_client = Session.SessionIdClient,
                        date_of_issue = null,
                        date_of_order = null,
                        delivery_address = null,
                    };


                    products_In_Order = new Products_in_order
                    {
                        id_order = order.id_order,
                        id_product = _currrentProduct.id_product,
                        count_of_products = int.Parse(Count_TextBox.Text),
                    };

                    PetshopEntities.GetContext().Orders.Add(order);
                    PetshopEntities.GetContext().Products_in_order.Add(products_In_Order);
                }
                else
                {
                    var isNewProduct = isNewProducts.FirstOrDefault(inp => inp.id_product == _currrentProduct.id_product && inp.id_order == isNewOrder.id_order);
                    if (isNewProduct != null)
                    {
                        //Products_in_order thisProduct = PetshopEntities.GetContext().Products_in_order.FirstOrDefault(p => p.id_product == _currrentProduct.id_product);
                        isNewProduct.count_of_products += int.Parse(Count_TextBox.Text);
                    }
                    else
                    {
                        products_In_Order = new Products_in_order
                        {
                            id_order = isNewOrder.id_order,
                            id_product = _currrentProduct.id_product,
                            count_of_products = int.Parse(Count_TextBox.Text),
                        };

                        PetshopEntities.GetContext().Products_in_order.Add(products_In_Order);
                    }
                }

                PetshopEntities.GetContext().SaveChanges();
                MessageBox.Show("Товар добавлен в корзину");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GoBack_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
