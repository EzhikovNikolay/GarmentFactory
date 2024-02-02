using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TestConnect
{
    public partial class Cabinet : Page
    {
        public ListView ProductListView
        {
            get { return productListView; }
        }
        public Cabinet()
        {
            InitializeComponent();
            PopulateProductListView();
        }
        public void PopulateProductListView()
        {
            string connectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=GarmentFactory;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT VendorCode, Size, Color, Price FROM Stock ORDER BY VendorCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Product> products = new List<Product>();
                        while (reader.Read())
                        {
                            string vendorCode = reader.GetString(0);
                            string size = reader.GetString(1);
                            string color = reader.GetString(2);
                            string price = reader.GetString(3);
                            products.Add(new Product { VendorCode = vendorCode, Size = size, Color = color, Price = price });
                        }

                        productListView.ItemsSource = products;
                    }
                }
            }
        }
        public class Product
        {
            public string VendorCode { get; set; }
            public string Size { get; set; }
            public string Color { get; set; }
            public string Price { get; set; }
        }
        private void EditClick(object sender, EventArgs e)
        {
            try
            {


                string connectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=GarmentFactory;Integrated Security=True";
                string updateQuery = "UPDATE Stock SET Size = @NewSize, Color = @NewColor, Price = @NewPrice WHERE VendorCode = @NewVendorCode";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@NewVendorCode", VendorCodeBox.Text);
                        command.Parameters.AddWithValue("@NewSize", SizeBox.Text);
                        command.Parameters.AddWithValue("@NewColor", ColorBox.Text);
                        command.Parameters.AddWithValue("@NewPrice", PriceBox.Text);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
        private void UpdateProduct(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-C2SIN3V;Initial Catalog=GarmentFactory;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT VendorCode, Size, Color, Price FROM Stock ORDER BY VendorCode";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Product> products = new List<Product>();
                        while (reader.Read())
                        {
                            string vendorCode = reader.GetString(0);
                            string size = reader.GetString(1);
                            string color = reader.GetString(2);
                            string price = reader.GetString(3);
                            products.Add(new Product { VendorCode = vendorCode, Size = size, Color = color, Price = price });
                        }

                        productListView.ItemsSource = products;
                    }
                }
            }
        }
    }
}
