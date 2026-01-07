using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateCode.Models;

namespace TemplateCode.DataAcces
{
    class DalSQL
    {
        //Eigenschappen
        private string serverName;
        private string databaseName;
        private string connectionString;

        //Constructor
        public DalSQL()
        {
            serverName = "LAPTOP-5FM0T3FM";
            databaseName = "Bestelsysteem";
            connectionString = $"Server={serverName};Database={databaseName};Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public List<Product> GetAllProducts()
        {
            string query = "SELECT Id, Name, Price FROM Product";
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string productName = reader.GetString(1);
                        decimal productPrice = reader.GetDecimal(2);

                        var product = new Product(id, productName, productPrice);

                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }

}
