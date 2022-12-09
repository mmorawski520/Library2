using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookWindow bookwindow = new BookWindow();
            bookwindow.Show();
            this.Close();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.Show();
            this.Close();
        }

        private void Rent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Show();
            this.Close();
        }

        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AuthorWindow authorWindow = new AuthorWindow();
            authorWindow.Show();
            this.Close();
        }
    }
}
