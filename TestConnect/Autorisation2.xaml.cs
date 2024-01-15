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
using System.Windows.Shapes;

namespace TestConnect
{
    /// <summary>
    /// Логика взаимодействия для Autorisation2.xaml
    /// </summary>
    public partial class Autorisation2 : Window
    {
        public const string ConnectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=Test;Integrated Security=True";
        public Autorisation2()
        {
            InitializeComponent();
        }

        public void LoginBox(string username, string password)
        {
            string Code = CodeBox.Text;

            string Password = PasswordBox.Password;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                {
                    try
                    {
                        SqlCommand command = new SqlCommand("SELECT Login, Password, Code FROM [User] WHERE Login = @ULogin AND Password = @UPassword AND Code = @UCode", connection); // SQL команда 
                        command.Parameters.AddWithValue("@ULogin", Login);
                        command.Parameters.AddWithValue("@UPassword", Password);
                        command.Parameters.AddWithValue("@UCode", Code);
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
                            MessageBox.Show("Неправильно введены данные");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: ", ex.Message);
                    }
                }
            }
        }
        
    }
}
