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

namespace TestConnect
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
        private void RegisterUser(string Login, string Password)
        {
            const string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=GarmentFactory;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO [User] (Login, Password) VALUES (@Login, @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", Login);
                        command.Parameters.AddWithValue("@Password", Password);

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
            string Login = UserLogin.Text;
            string Password = UserPassword.Password;

            RegisterUser (Login, Password);
        }

      
    }
}
