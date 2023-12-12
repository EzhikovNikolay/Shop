using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для AdminAdd.xaml
    /// </summary>
    public partial class AdminAdd : Page
    {
        public AdminAdd()
        {
            InitializeComponent();
            PopulateProductListView();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Shop;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO [Product] (ProductArticleNumber, ProductName, ProductDescription, ProductCategory, ProductPhoto, ProductQuantityInStock, ProductCost, ProductManufacturer, ProductDiscountAmount) VALUES (@Article, @Name, @Description, @Category ,@Photo, @Quantity, @Cost, @Manufacturer, @Discount)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Article", article.Text);
                        command.Parameters.AddWithValue("@Name", name.Text);
                        command.Parameters.AddWithValue("@Description", description.Text);
                        command.Parameters.AddWithValue("@Category", category.Text);
                        command.Parameters.AddWithValue("@Photo", photo.Text);
                        command.Parameters.AddWithValue("@Quantity", quantity.Text);
                        command.Parameters.AddWithValue("@Cost", price.Text);
                        command.Parameters.AddWithValue("@Manufacturer", provider.Text);
                        command.Parameters.AddWithValue("@DIscount", discount.Text);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }

            }
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
        private void Back_Click(object sender, EventArgs e)
        {
            LoadPage("AdminCabinet.xaml");
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
