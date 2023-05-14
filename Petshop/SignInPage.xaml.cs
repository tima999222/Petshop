using System.Windows;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class SignInPage : Window
    {
        public SignInPage()
        {
            InitializeComponent();
            
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new LoginPage());
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new RegistrationPage());
        }
    }
}
