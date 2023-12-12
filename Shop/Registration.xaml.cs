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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void RegisterUser(string Surname, string Name, string Patronymic, string Login, string Password, string Role)
        {
            const string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Shop;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO [User] (UserSurname, UserName, UserPatronymic, UserLogin, UserPassword, UserRole) VALUES (@LastName, @FirstName, @MiddleName, @Username, @Password, @Role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LastName", Surname);
                        command.Parameters.AddWithValue("@FirstName", Name);
                        command.Parameters.AddWithValue("@MiddleName", Patronymic);
                        command.Parameters.AddWithValue("@Username", Login);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Role", Role);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при регистрации пользователя: " + ex.Message);
                }
            }
        }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            string Surname = UserSurname.Text;
            string Name = UserName.Text;
            string Patronymic = UserPatronymic.Text;
            string Login = UserLogin.Text;
            string Password = UserPassword.Password;
            string Role = "Клиент";

            RegisterUser(Surname, Name, Patronymic, Login, Password, Role);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Login.xaml");
            void LoadPage(string pageName)
            {
                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                framereg.Content = page;
                page.Background = this.Background;
                page.Width = this.Width;
                page.Height = this.Height;
            };
        }
    }
}


