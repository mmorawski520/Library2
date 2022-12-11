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
        DbHelper dbHelper = new DbHelper();
        int index;
        bool isEdited = false;
        public CategoryWindow()
        {
            InitializeComponent();
            initCategories();
        }
        private bool displayError()
        {
            if (txtBoxName.Text == "" )
                return true;
            return false;
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void initCategories()
        {
            listView.ItemsSource = dbHelper.getCategory();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!displayError())
            {

                if (!isEdited)
                    dbHelper.addCategory(txtBoxName.Text);
                else
                {
                    dynamic selectedItem = listView.Items[index];
                    dbHelper.updateCategory(Convert.ToString(selectedItem["id"]), txtBoxName.Text);
                   
                }
                txtBoxName.Text = "";
               
                isEdited = false;

                initCategories();
            }
            else
            {
                MessageBox.Show("Invalid data");
            }

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = (int)listView.SelectedIndex;

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            dynamic selectedItem = listView.SelectedItem;
            var name = selectedItem["name"].ToString();
            
            txtBoxName.Text = name;
           

            isEdited = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = listView.SelectedItem;
            dbHelper.deleteCategory(Convert.ToString(selectedItem["id"]));
            initCategories();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
