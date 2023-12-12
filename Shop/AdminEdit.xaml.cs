﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace Shop
{
    /// <summary>
    /// Логика взаимодействия для AdminEdit.xaml
    /// </summary>
    public partial class AdminEdit : Page
    {
        public AdminEdit()
        {
            InitializeComponent();
            PopulateProductListView();

        }
        private void PopulateProductListView()
        {
            string connectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Shop;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductArticleNumber, ProductPhoto, ProductName, ProductDescription FROM Product";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Product> products = new List<Product>();
                        while (reader.Read())
                        {
                            string article = reader.GetString(0);
                            string imagePath = reader.GetString(1);
                            string name = reader.GetString(2);
                            string description = reader.GetString(3);
                            products.Add(new Product {Article = article, ImagePath = imagePath, Name = name, Description = description });
                        }
                        StringBuilder sb = new StringBuilder();
                        foreach (Product product in products)
                        {
                            sb.Append($"Article: {product.Article}, Image Path: {product.ImagePath}, Name: {product.Name}, Description: {product.Description}\n");
                        }
                        productListView.Items.Clear();
                        productListView.ItemsSource = products;
                    }
                }
            }
        }
        public class Product
        {
            public string Article { get; set; }
            public string ImagePath { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }



        private void Edit(object sender, EventArgs e)
        {
            {
                try
                {


                    string connectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Shop;Integrated Security=True";
                    string updateQuery = "UPDATE Product SET ProductPhoto = @NewPhoto, ProductName = @NewName, ProductDescription = @NewDescription WHERE ProductArticleNumber = @Article";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@NewPhoto", photo.Text); 
                            command.Parameters.AddWithValue("@NewName", name.Text);
                            command.Parameters.AddWithValue("@NewDescription", description.Text); 
                            command.Parameters.AddWithValue("@Article", article.Text);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            LoadPage("AdminCabinet.xaml");
            void LoadPage(string pageName)
            {
                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                frameedit.Content = page;
                page.Background = this.Background;
                page.Width = this.Width;
                page.Height = this.Height;
            };
        }
    }
}
