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
using System.Windows.Shapes;

namespace Library2
{
    /// <summary>
    /// Logika interakcji dla klasy AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window
    {
        DbHelper dbHelper= new DbHelper();
        public AuthorWindow()
        {
            InitializeComponent();
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            dbHelper.addAuthor(txtBoxName.Text,txtBoxSurname.Text);
            txtBoxName.Text = "";
            txtBoxSurname.Text = "";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
