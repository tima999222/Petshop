using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    /// 

    public partial class RegistrationPage : Page
    { 
        private Clients _currentClient = new Clients();
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Control> textBoxes = new List<Control>();

            loginTextBox.Rules().MinCharacters(1).Validate().addToList(textBoxes);
            passwordTextBox.Rules().MinCharacters(5).Validate().addToList(textBoxes);
            passwordShowTextBox.Rules().MinCharacters(5).Validate().addToList(textBoxes);
            surnameTextBox.Rules().MinCharacters(1).Validate().addToList(textBoxes);
            nameTextBox.Rules().MinCharacters(1).Validate().addToList(textBoxes);
            mailTextBox.Rules().isEmail().Validate().addToList(textBoxes);
            phoneTextBox.Rules().isPhone().Validate().addToList(textBoxes);

            if (textBoxes.Count < 7)
            {
                textBoxes.Clear();
                return;
            }

            _currentClient.surname = surnameTextBox.Text;
            _currentClient.name = nameTextBox.Text;
            _currentClient.patronymic = patronymicTextBox.Text;
            if (patronymicTextBox.Text == "")
            {
                _currentClient.patronymic = null;
            }
            _currentClient.e_mail = mailTextBox.Text;
            _currentClient.phone_number = phoneTextBox.Text;
            _currentClient.login = loginTextBox.Text;
            _currentClient.password = (string)MD5Hash.CalculateMD5Hash(passwordTextBox.Password);

            if (_currentClient.id_client == 0)
            {
                PetshopEntities.GetContext().Clients.Add(_currentClient);
            }
            try
            {
                PetshopEntities.GetContext().SaveChanges();
                MessageBox.Show("Вы были зарегистрированы");
                NavigationService.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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
