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
        int index;
        bool isEdited = false;
        public AuthorWindow()
        {
            InitializeComponent();
            initAuthors();
        }

        private bool displayError()
        {
            if(txtBoxName.Text == "" || txtBoxSurname.Text =="")
                return true;
            return false;
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void initAuthors() {
            listView.ItemsSource = dbHelper.getAuthor();
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
                    dbHelper.addAuthor(txtBoxName.Text, txtBoxSurname.Text);
                else
                {
                    dynamic selectedItem = listView.Items[index];
                    dbHelper.updateAuthor(Convert.ToString(selectedItem["id"]), txtBoxName.Text, txtBoxSurname.Text);
                }
                    txtBoxName.Text = "";
                    txtBoxSurname.Text = "";
                    isEdited = false;

                initAuthors();
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
            String fullname = selectedItem["fullname"].ToString();
            string[] fullnames = fullname.Split(' ');
            txtBoxName.Text = fullnames[0];
            txtBoxSurname.Text = fullnames[1]; 

           isEdited= true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = listView.SelectedItem;
            dbHelper.deleteAuthor(Convert.ToString(selectedItem["id"]));
            initAuthors();
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
