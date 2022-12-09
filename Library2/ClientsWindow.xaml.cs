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
    /// Logika interakcji dla klasy ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        DbHelper dbHelper = new DbHelper();
        int index;
        bool add = true;
        public ClientsWindow()
        {
            InitializeComponent();
            initUser();
        }

        private void initUser()
        {
            listView.ItemsSource = dbHelper.getUser();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
        private bool displayError()
        {
            if (txtBoxName.Text=="")
            {
                MessageBox.Show("Invalid form data");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (displayError())
            {
                if (add)
                {
                    dbHelper.addUser(txtBoxName.Text);
                }
                  
                else
                {
                    try
                    {
                        dynamic selectedItem = listView.Items[index];
                       
                        dbHelper.updateUser(txtBoxName.Text, Convert.ToString(selectedItem["id"]));
                    }
                    catch(Exception eer) { }
                }
          
               

                add = true;
               
                this.initUser();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = listView.SelectedItem;
            dbHelper.removeUser(Convert.ToString(selectedItem["id"]));
            initUser();
         
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = (int)listView.SelectedIndex;

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = listView.SelectedItem;
            txtBoxName.Text = selectedItem["name"].ToString();
           
            
            add = false;
        }
    }
}
