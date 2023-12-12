﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для AdminCabinet.xaml
    /// </summary>
    public partial class AdminCabinet : Page
    {
        public AdminCabinet()
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
                            products.Add(new Product { ProductArticleNumber = article, ImagePath = imagePath, Name = name, Description = description });
                        }
                        productListView.ItemsSource = products;
                    }
                }
            }
        }
        public class Product
        {
            public string ProductArticleNumber { get; set; }
            public string ImagePath { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
        private void DeleteProductFromDatabase(string article)
        {
            string connectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Shop;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Product WHERE ProductArticleNumber = @Article";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Article", article);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (productListView.SelectedItem != null)
            {
                var selectedProduct = productListView.SelectedItem as Product;
                // Удаление выбранного продукта из базы данных
                DeleteProductFromDatabase(selectedProduct.ProductArticleNumber);
                // Перезагрузка данных в ListView
                PopulateProductListView();
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("AdminAdd.xaml");
            void LoadPage(string pageName)
            {
                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                frameadd.Content = page;
                page.Background = this.Background;
                page.Width = this.Width;
                page.Height = this.Height;
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            
                try
                {
                    LoadPage("AdminEdit.xaml");
                    void LoadPage(string pageName)
                    {
                        Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                        frameadd.Content = page;
                        page.Background = this.Background;
                        page.Width = this.Width;
                        page.Height = this.Height;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);                   
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Login.xaml");
            void LoadPage(string pageName)
            {
                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                frameadd.Content = page;
                page.Background = this.Background;
                page.Width = this.Width;
                page.Height = this.Height;
            };
        }
    }
}
