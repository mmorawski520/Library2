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
    /// Logika interakcji dla klasy BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        DbHelper dbHelper = new DbHelper();
        int index;
        bool add=true;

        public BookWindow()
        {
            InitializeComponent();
            initCategories();
            initAuthors();
            initBooks();
        }

        private void initCategories()
        {
            cmbBoxCategory.ItemsSource = dbHelper.getCategory();
            cmbBoxCategory.DisplayMemberPath = "name";
            cmbBoxCategory.SelectedValuePath = "id";
        }

        private void initAuthors()
        {
            cmbBoxAuthor.ItemsSource = dbHelper.getAuthor();
            cmbBoxAuthor.DisplayMemberPath = "fullname";
            cmbBoxAuthor.SelectedValuePath = "id";
        }

        private void initBooks()
        {
            listView.ItemsSource = dbHelper.getBook();
        }

    

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (displayError())
            {
                if (add)
                    dbHelper.addBook(txtBoxTitle.Text, txtBoxDesc.Text, cmbBoxCategory.SelectedValue.ToString(), cmbBoxAuthor.SelectedValue.ToString(
                        ));
                else
                {
                    dynamic selectedItem = listView.Items[index];
                    dbHelper.updateBook(Convert.ToString(selectedItem["id"]), txtBoxTitle.Text, txtBoxDesc.Text, cmbBoxCategory.SelectedValue.ToString(), cmbBoxAuthor.SelectedValue.ToString(
                        ));
                }
                txtBoxTitle.Text = "";
                txtBoxDesc.Text = "";
                cmbBoxCategory.SelectedItem = null;
                cmbBoxAuthor.SelectedItem = null;

                add = true;
                this.initBooks();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem =listView.SelectedItem;
            dbHelper.deleteBook(Convert.ToString(selectedItem["id"]));
            initBooks();
        }

        private bool displayError()
        {
            if (txtBoxTitle.Text == "" || txtBoxDesc.Text == "" || cmbBoxCategory.SelectedValue.ToString() == "" || cmbBoxAuthor.SelectedValue.ToString(
                    ) == "")
            {
                MessageBox.Show("Invalid form data");
                return false;
            }
            return true;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = listView.SelectedItem;
            txtBoxTitle.Text = selectedItem["name"].ToString();
            txtBoxDesc.Text = selectedItem["description"].ToString();
           
            cmbBoxAuthor.SelectedItem = selectedItem["author"].ToString();
            cmbBoxCategory.SelectedItem = selectedItem["category"].ToString();
            add = false;
        
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = (int)listView.SelectedIndex;
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
