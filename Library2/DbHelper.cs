using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Library2
{
    internal class DbHelper
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=data;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection sqlConnection;

/******************************************
        method name: startConnection()
        arguments: none
        output: none
        desc: method starts sql connection
        author: jakis pesel
******************************************/

        public void startConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

/******************************************
   method name: addCategory()
   arguments: name - string
   output: none
   desc: method adds category into db
   author: jakis pesel
******************************************/
        public void addCategory(string name)
        {

            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO categories(name) values('" + name + "')", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
   method name: updateCategory()
   arguments: id - string,name - string
   output: none
   desc: method updates category
   author: jakis pesel
******************************************/
        public void updateCategory(string id, string name)
        {

            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE categories set name='" + name + "' WHERE id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
   method name: addAuthor()
   arguments: name - string,surname - string
   output: none
   desc: method adds author
   author: jakis pesel
******************************************/
        public void addAuthor(string name, string surname)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO authors(name,surname) values('" + name + "','" + surname + "')", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
   method name: updateAuthor()
   arguments: id - string,name - string,surname - string
   output: none
   desc: method updates author
   author: jakis pesel
******************************************/

        public void updateAuthor(string id, string name, string surname)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE authors set name='" + name + "', surname='" + surname + "' WHERE id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
   method name: deleteAuthor()
   arguments: id - string
   output: none
   desc: method deletes author
   author: jakis pesel
******************************************/

        public void deleteAuthor(string id)
        {
            startConnection();
            sqlConnection.Open();
            try
            {
                var cmd = new SqlCommand("DELETE FROM authors where id=" + id, sqlConnection);
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                var cmd = new SqlCommand("UPDATE authors set name='deleted', surname='author' WHERE id=" + id, sqlConnection);
                cmd.ExecuteScalar();
            }

            sqlConnection.Close();
        }

/******************************************
  method name: deleteCategory()
  arguments: id - string
  output: none
  desc: method deletes category
  author: jakis pesel
******************************************/

        public void deleteCategory(string id)
        {
            startConnection();
            sqlConnection.Open();
            try
            {
                var cmd = new SqlCommand("DELETE FROM categories where id=" + id, sqlConnection);
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                var cmd = new SqlCommand("UPDATE categories set name='deleted' WHERE id=" + id, sqlConnection);
                cmd.ExecuteScalar();
            }

            sqlConnection.Close();
        }

/******************************************
  method name: getCategory()
  arguments: id - string
  output: category:SqlAdapter
  desc: method returns categories
  author: jakis pesel
******************************************/

        public dynamic getCategory()
        {
            startConnection();
            sqlConnection.Open();
            var adapter = new SqlDataAdapter("SELECT id,name FROM categories where name not like 'deleted'", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t");
            sqlConnection.Close();
            return ds.Tables["t"].DefaultView;
        }

/******************************************
  method name:  getAuthor()
  arguments: id - string
  output: category:SqlAdapter
  desc: method returns authors
  author: jakis pesel
******************************************/

        public dynamic getAuthor()
        {
            startConnection();
            sqlConnection.Open();
            var adapter = new SqlDataAdapter("SELECT id,CONCAT(name,' ',surname) as fullname FROM authors WHERE name not like 'deleted'", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView;
        }

/******************************************
  method name: addBook()
  arguments: name - string,desc - string,id_c - string,id_a - string
  output: none
  desc: method adds book into db
  author: jakis pesel
******************************************/

        public void addBook(string name, string desc, string id_c, string id_a)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO books(name,description,id_c,id_a,created_at) values('" + name + "','" + desc + "'," + id_c + "," + id_a + ",'" + DateTime.UtcNow.ToString() + "')", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
  method name:  getBook()
  arguments: id - string
  output: category:SqlAdapter
  desc: method returns books
  author: jakis pesel
******************************************/

        public dynamic getBook()
        {
            startConnection();
            sqlConnection.Open();
            var adapter = new SqlDataAdapter("SELECT books.id as id,books.name as name, books.description as description,categories.name as category,CONCAT(authors.name,' ',surname) as author FROM books inner join categories ON books.id_c = categories.id inner join authors ON books.id_a = authors.id where books.name not like 'deleted'", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView;
        }

/******************************************
  method name: deleteBook()
  arguments: id - string
  output: none
  desc: method deletes book
  author: jakis pesel
******************************************/

        public void deleteBook(string id)
        {
            startConnection();
            sqlConnection.Open();
            try
            {
                var cmd = new SqlCommand("DELETE FROM books where id=" + id + "", sqlConnection);
                cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                var cmd = new SqlCommand("UPDATE books set name='deleted' WHERE id=" + id, sqlConnection);
                cmd.ExecuteScalar();
            }


            sqlConnection.Close();
        }

/******************************************
  method name: updateBook()
  arguments: id - string,name - string,desc - string,id_a - string,id_c - string
  output: none
  desc: method updates book
  author: jakis pesel
******************************************/

        public void updateBook(string id, string name, string desc, string id_a, string id_c)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE books set name='" + name + "', description='" + desc + "',id_a=" + id_a + ",id_c=" + id_c + "WHERE id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
  method name: addUser()
  arguments: name - string
  output: none
  desc: method adds user into db
  author: jakis pesel
******************************************/

        public void addUser(string name)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT into users(name,password,isAdmin) values('" + name + "','',0)", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
  method name: updateUser()
  arguments: id - string,name - string
  output: none
  desc: method updates user
  author: jakis pesel
******************************************/

        public void updateUser(string name, string id)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE users set name='" + name + "' WHERE id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
  method name: removeUser()
  arguments: id - string
  output: none
  desc: method deletes User
  author: jakis pesel
******************************************/

        public void removeUser(string id)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("DELETE FROM orders WHERE id_u=" + id, sqlConnection);
            cmd.ExecuteScalar();
            cmd = new SqlCommand("DELETE FROM users WHERE id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
  method name: getUser()
  arguments: id - string
  output: category:SqlAdapter
  desc: method returns users
  author: jakis pesel
******************************************/

        public dynamic getUser()
        {
            startConnection();
            sqlConnection.Open();
            var adapter = new SqlDataAdapter("SELECT id,name FROM users", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView;
        }

/******************************************
  method name: getOrders()
  arguments: id - string
  output: category:SqlAdapter
  desc: method returns orders
  author: jakis pesel
******************************************/

        public dynamic getOrders()
        {
            startConnection();
            sqlConnection.Open();
            var adapter = new SqlDataAdapter("SELECT orders.id as id,books.name as book_name,users.name as rented_by,rented_at as rented_at,rented_to as rented_to,is_returned as is_returned FROM borrowed_books INNER JOIN books ON borrowed_books.id_b = books.id INNER JOIN orders ON borrowed_books.id_o = orders.id INNER JOIN  users ON orders.id_u = users.id", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView;

        }

/******************************************
  method name: addOrders() 
  arguments: id_u - string,id_b - string,rented_to - string,is_returned - string
  output: none
  desc: method adds category into db
  author: jakis pesel
******************************************/

        public void addOrders(string id_u, string id_b, string rented_to, int is_returned)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO orders(id_u,created_at) VALUES(" + id_u + ",getdate()); SELECT SCOPE_IDENTITY()", sqlConnection);
            var lastRecord = cmd.ExecuteScalar().ToString();
            cmd = new SqlCommand("INSERT INTO borrowed_books(id_o,id_b,rented_at,rented_to,is_returned) VALUES(" + lastRecord + "," + id_b + ",getdate(),'" + rented_to + "'," + is_returned.ToString() + ")", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

 /******************************************
  method name: updateOrders()
  arguments: id - string,id_u - string,id_b - string,rented_to - string,is_returned - string
  output: none
  desc: method updates order
  author: jakis pesel
 ******************************************/

        public void updateOrders(string id, string id_u, string id_b, string rented_to, string is_returned)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE borrowed_books set id_b=" + id_b + ",rented_to='" + rented_to + "',is_returned=" + is_returned + " FROM  borrowed_books INNER JOIN orders ON borrowed_books.id_o = orders.id WHERE borrowed_books.id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            cmd = new SqlCommand("UPDATE orders set id_u=" + id_u + " where id=" + id + "", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

/******************************************
  method name: deleteOrders()
  arguments: id - string
  output: none
  desc: method deletes order
  author: jakis pesel
******************************************/

        public void deleteOrders(string id)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("DELETE FROM borrowed_books WHERE id_o=" + id, sqlConnection);
            cmd.ExecuteScalar();
            cmd = new SqlCommand("DELETE FROM orders WHERE id=" + id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();

        }
    }
}
