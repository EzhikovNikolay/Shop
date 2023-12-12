using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private int failedAttempts = 0;
        public static class DataStorage
        {
            public static string Login { get; set; }
        }

        
        private const string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Shop;Integrated Security=True";
        public Login()
        {
            InitializeComponent();
        }
        

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string Login = usernameTextBox.Text;
            string Password = passwordBox.Password;
            string enteredCaptcha = CaptchaTextBox.Text;

            Random random = new Random();
            string correctCaptcha = "";
            for (int i = 0; i < 4; i++)
            {
                correctCaptcha += Convert.ToChar(random.Next(65, 90));
            }
            CaptchaVieW.Text = correctCaptcha;

            if (AuthenticateUser(Login, Password))
            {
            }
            else
            {
                failedAttempts++;
                if (failedAttempts == 1)
                {
                    CaptchaVieW.Visibility = Visibility.Visible;
                    CaptchaLabel.Visibility = Visibility.Visible;
                    CaptchaTextBox.Visibility = Visibility.Visible;
                }
                else
                    if (enteredCaptcha != correctCaptcha)
                {
                    MessageBox.Show("Вы неправильно ввели Captcha и были заблокированы на 10 секунд");
                    Thread.Sleep(10000);  
                    return;
                }
            }
            bool AuthenticateUser(string login, string password)
            {
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("SELECT UserLogin, UserRole FROM [User] WHERE UserLogin = @Login AND UserPassword = @Password", connection);
                            command.Parameters.AddWithValue("@Login", login);
                            command.Parameters.AddWithValue("@Password", password);

                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string userLogin = reader.GetString(0);
                                    string userRole = reader.GetString(1);

                                    if (userRole == "Администратор")
                                    {
                                        LoadPage("AdminCabinet.xaml");
                                         void LoadPage(string pageName)
                                        {
                                            Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                                            framelog.Content = page;
                                            page.Background = this.Background;
                                            page.Width = this.Width;
                                            page.Height = this.Height;
                                        }
;
                                    }
                                    if (userRole == "Менеджер")
                                    {
                                        LoadPage("ClientCabinet.xaml");
                                        void LoadPage(string pageName)
                                        {
                                            Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                                            framelog.Content = page;
                                            page.Background = this.Background;
                                            page.Width = this.Width;
                                            page.Height = this.Height;
                                        }
                                    }
                                    if (userRole == "Клиент")
                                    {
                                        LoadPage("ClientCabinet.xaml");
                                        void LoadPage(string pageName)
                                        {
                                            Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                                            framelog.Content = page;
                                            page.Background = this.Background;
                                            page.Width = this.Width;
                                            page.Height = this.Height;
                                        }
                                    }
                                    return true;
                                }
                            }
                            MessageBox.Show("Пользователь не найден");
                            return false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message);
                            return false;
                        }
                    }
                }
            }
        }

        private void LoginGuest_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("GuestCabinet.xaml");
            void LoadPage(string pageName)
            {
                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                framelog.Content = page;
                page.Background = this.Background;
                page.Width = this.Width;
                page.Height = this.Height;
            };
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("Registration.xaml");
            void LoadPage(string pageName)
            {
                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                framelog.Content = page;
                page.Background = this.Background;
                page.Width = this.Width;
                page.Height = this.Height;
            };
        }
    }
}
