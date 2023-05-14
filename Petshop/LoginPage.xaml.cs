using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        static bool checkSuccess(object CurrentUser)
        {
            bool flag = true;
            if (CurrentUser != null)
            {
                MessageBox.Show("Успешно");
            }
            else
            {
                flag = false;
                MessageBox.Show("Неправильный логин или пароль");
            }

            return flag;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = "";
            if (passwordTextBox.Visibility == Visibility.Visible)
            {
                password = MD5Hash.CalculateMD5Hash(passwordTextBox.Password);
            }
            else
            {
                password = MD5Hash.CalculateMD5Hash(passwordShowTextBox.Text);
            }


            if (isAdmin.IsChecked == true)
            {   
                var CurrentUser = PetshopEntities.GetContext().Admins.FirstOrDefault(a => a.login == login && a.password == password);
                if (!checkSuccess(CurrentUser))
                    return;
                

                DataEditWindow dataEditWindow = new DataEditWindow();
                dataEditWindow.ShowDialog();
            }
            else
            {
                var CurrentUser = PetshopEntities.GetContext().Clients.FirstOrDefault(a => a.login == login && a.password == password);
                if (!checkSuccess(CurrentUser))
                    return;

                Session.SessionIdClient = CurrentUser.id_client;
                MainWindow productList = new MainWindow();
                if (productList.ShowDialog() == true)
                {
                    Session.SessionIdClient = 0;
                }
            }
        }

        private void CheckBox_Show(object sender, RoutedEventArgs e)
        {
            passwordShowTextBox.Text = passwordTextBox.Password;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordShowTextBox.Visibility = Visibility.Visible;
        }

        private void CheckBox_Hide(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Password = passwordShowTextBox.Text;
            passwordShowTextBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Visibility = Visibility.Visible;
        }
    }
}
