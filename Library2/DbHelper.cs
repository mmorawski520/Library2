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
        public void startConnection()
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public void addCategory(string name)
        {

            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO categories(name) values('" + name + "')", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

        public void addAuthor(string name, string surname)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO authors(name,surname) values('" + name + "','" + surname + "')", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

        public dynamic getCategory()
        {
            startConnection();
            sqlConnection.Open();

            var adapter = new SqlDataAdapter("SELECT id,name FROM categories", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "t");
            sqlConnection.Close();
            return ds.Tables["t"].DefaultView;
        }

        public dynamic getAuthor()
        {
            startConnection();
            sqlConnection.Open();

            var adapter = new SqlDataAdapter("SELECT id,CONCAT(name,' ',surname) as fullname FROM authors", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView; 
        }

        public void addBook(string name,string desc,string id_c,string id_a)
        {
            startConnection();
             sqlConnection.Open();
            var cmd = new SqlCommand("INSERT INTO books(name,description,id_c,id_a,created_at) values('"+name+"','"+desc+"',"+id_c+","+id_a+",'"+DateTime.UtcNow.ToString()+"')", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

        public dynamic getBook()
        {
            startConnection();
            sqlConnection.Open();
       
            var adapter = new SqlDataAdapter("SELECT books.id as id,books.name as name, books.description as description,categories.name as category,CONCAT(authors.name,' ',surname) as author FROM books inner join categories ON books.id_c = categories.id inner join authors ON books.id_a = authors.id", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView;
        }

        public void deleteBook(string id)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("DELETE FROM books where id="+id+"", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }


        public void deleteAuthor(string id)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("DELETE FROM authors where id=" + id + "", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

        public void deleteBCategory(string id)
        {
            startConnection();
            sqlConnection.Open();
            MessageBox.Show("DELETE FROM categories where id=" + id + "");
            var cmd = new SqlCommand("DELETE FROM categories where id=" + id + "", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

        public void updateBook(string id,string name,string desc,string id_a,string id_c) {

            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE books set name='"+name+"', description='"+desc+"',id_a="+id_a+",id_c="+id_c+"WHERE id="+id, sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();
        }

        public void addUser(string name)
        {
            startConnection();
            sqlConnection.Open();
            var cmd = new SqlCommand("INSERT into users(name,password,isAdmin) values('"+name+"','',0)", sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();


        }

        public void updateUser(string name,string id)
        {
            startConnection();
                sqlConnection.Open();
            var cmd = new SqlCommand("UPDATE users set name='" + name + "' WHERE id=" + id, sqlConnection);
                cmd.ExecuteScalar();
            sqlConnection.Close();

                        
        }
        public void removeUser(string id)
        {
            startConnection();
            sqlConnection.Open();
    
            var cmd = new SqlCommand("DELETE FROM users WHERE id="+id,sqlConnection);
            cmd.ExecuteScalar();
            sqlConnection.Close();


        }

        public dynamic getUser()
        {
            startConnection();
            sqlConnection.Open();

            var adapter = new SqlDataAdapter("SELECT * FROM users", sqlConnection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            sqlConnection.Close();
            return ds.Tables[0].DefaultView;
        }
    }
}
