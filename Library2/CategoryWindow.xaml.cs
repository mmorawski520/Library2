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
    /// Logika interakcji dla klasy CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        DbHelper dbHelper= new DbHelper();
        public CategoryWindow()
        {
            InitializeComponent();
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            dbHelper.addCategory(txtBoxName.Text);
            txtBoxName.Text = "";
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
