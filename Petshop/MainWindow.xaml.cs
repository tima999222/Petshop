using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Petshop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
            Session.SessionIdClient = 0;
        }
    }
}
