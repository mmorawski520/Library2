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
    /// Logika interakcji dla klasy RentWindow.xaml
    /// </summary>
    public partial class RentWindow : Window
    {
        DbHelper dbHelper = new DbHelper();
            int index;
        bool isEdited = false;

        public RentWindow()
        {
            InitializeComponent();
            initOrder();
            initBooks();
            initUsers();
        }

        private void initOrder()
        {
            listView.ItemsSource = dbHelper.getOrders();
        }

        private void initBooks()
        {
            cmbBoxBook.ItemsSource = dbHelper.getBook();
            cmbBoxBook.DisplayMemberPath = "name";
            cmbBoxBook.SelectedValuePath = "id";
        }

        private void initUsers()
        {
            cmbBoxUser.ItemsSource = dbHelper.getUser();
            cmbBoxUser.DisplayMemberPath = "name";
            cmbBoxUser.SelectedValuePath = "id";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main= new MainWindow();
            main.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? selectedDate = datepicker.SelectedDate;
                if (selectedDate.HasValue)
                {
                    if (!isEdited)
                    {
                        string formatted = selectedDate.Value.ToString("yyyy.MM.dd", System.Globalization.CultureInfo.InvariantCulture); ;
                        this.dbHelper.addOrders(cmbBoxUser.SelectedValue.ToString(), cmbBoxBook.SelectedValue.ToString(), formatted, 0);
 
                    }
                    else
                    {
                        string formatted = selectedDate.Value.ToString("yyyy.MM.dd", System.Globalization.CultureInfo.InvariantCulture); ;
                        dynamic selectedItem = listView.Items[index];
                        if(isReturned.IsChecked == true)
                        this.dbHelper.updateOrders(Convert.ToString(selectedItem["id"]), cmbBoxUser.SelectedValue.ToString(), cmbBoxBook.SelectedValue.ToString(), formatted, "1");
                        else
                            this.dbHelper.updateOrders(Convert.ToString(selectedItem["id"]), cmbBoxUser.SelectedValue.ToString(), cmbBoxBook.SelectedValue.ToString(), formatted, "0");
                        
                        isEdited = false;
                    
                    }
                    cmbBoxBook.SelectedItem = null;
                    cmbBoxUser.SelectedItem = null;
                    isReturned.IsChecked = false;
                    datepicker.SelectedDate = DateTime.Now;
                }
                this.initOrder();
            }
            catch(Exception ex)
            {
              
                MessageBox.Show("Invalid data");
            }
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            isEdited = true;
            dynamic selectedItem = listView.Items[index];
            cmbBoxBook.SelectedItem = selectedItem["book_name"];
            cmbBoxUser.SelectedItem = selectedItem["rented_by"];
            datepicker.SelectedDate = DateTime.Now;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dynamic selectedItem = listView.SelectedItem;
            dbHelper.deleteOrders(Convert.ToString(selectedItem["id"]));
            initOrder();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            index = (int)listView.SelectedIndex;
        }
    }
}
