using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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

namespace TestConnect
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public const string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=GarmentFactory;Integrated Security=True"; // Подключение к БД
        public Autorization()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string Login = LoginBox.Text;
            string Password = PasswordBox.Password;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                {
                    try
                    {
                        SqlCommand command = new SqlCommand("SELECT Login, Password FROM [User] WHERE Login = @Login AND Password = @Password", connection); // SQL команда 
                        command.Parameters.AddWithValue("@Login", Login);
                        command.Parameters.AddWithValue("@Password", Password);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            LoadPage("Cabinet.xaml");
                            void LoadPage(string pageName)
                            {
                                Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
                                framelog.Content = page;
                                page.Background = this.Background;
                                page.Width = this.Width;
                                page.Height = this.Height;

                                // Переход на окно
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите правильный логин или пароль");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: ", ex.Message);
                    }
                }
            }
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
            }
        }
    }
}
