using System.Windows;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для SelectAddressWindow.xaml
    /// </summary>
    public partial class SelectAddressWindow : Window
    {
        public SelectAddressWindow()
        {
            InitializeComponent();
        }

        public string Address { get { return Address_TextBox.Text; } }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Address_TextBox.Text))
            {
                MessageBox.Show("Вы не ввели адрес");
                this.DialogResult = false;
                return;
            }

            this.DialogResult = true;
        }
    }
}
